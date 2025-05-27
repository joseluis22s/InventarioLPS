using System.Text.Json;
using InventarioLPS.Data;
using InventarioLPS.Data.Entities;
using InventarioLPS.Models.Inventario;
using InventarioLPS.Services.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace InventarioLPS.Controllers
{
    public class InventarioController : BaseController
    {
        private readonly InventarioLPSContext _context;

        public InventarioController(InventarioLPSContext context)
        {
            _context = context;

        }
        // GET: InventarioController
        public async Task<IActionResult> Index()
        {
            try
            {
                List<ItemInventario> inventario = await _context.ItemInventario
                                .Include(i => i.IdInformacionRegistroNavigation.IdFormaRegistroNavigation)
                                .Include(i => i.CodigoProductoNavigation)
                                .ToListAsync();

                var model = InventarioMapping.ToInventarioViewModelList(inventario);

                return View(model);
            }
            catch (Exception ex)
            {
                return ShowErrorPageFromException(ex);
            }
        }

        // GET: Inventario/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                var model = await BuildCreateRegistroViewModelAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                return ShowErrorPageFromException(ex);
            }
        }


        // POST: InventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRegistroInventarioViewModel model)
        {
            try
            {
                RemoveNonValidateFieldsCreation(model);
                if (!ModelState.IsValid)
                {
                    CreateRegistroInventarioViewModel modelInvalid = await BuildCreateRegistroViewModelAsync();
                    modelInvalid.Items = model.Items;
                    ModelState.AddModelError("", "Existen campos no válidos.");
                    return View(modelInvalid);
                }

                List<string> codigosItem = model.Items
                    .Select(i => i.CodigoItem)
                    .ToList();

                List<string> existingCodigosItem = await _context.ItemInventario
                    .Where(i => codigosItem.Contains(i.CodigoItem))
                    .Select(i => i.CodigoItem)
                    .ToListAsync();

                if (existingCodigosItem.Any())
                {
                    CreateRegistroInventarioViewModel modelInvalid = await BuildCreateRegistroViewModelAsync();
                    modelInvalid.Items = model.Items;
                    for (int i = 0; i < model.Items.Count; i++)
                    {
                        var codigo = model.Items[i].CodigoItem;
                        if (existingCodigosItem.Contains(codigo))
                        {
                            ModelState.AddModelError($"Items[{i}].CodigoItem", $"Código ya existente.");
                        }
                    }
                    return View(modelInvalid);
                }

                InformacionRegistro newDataRegister = InventarioMapping.ToInformacionRegisterEntity(model);
                await _context.InformacionRegistro.AddAsync(newDataRegister);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return ShowErrorPageFromException(ex);
            }
        }
        private void RemoveNonValidateFieldsCreation(CreateRegistroInventarioViewModel model)
        {
            for (int i = 0; i < model.Items.Count; i++)
            {
                ModelState.Remove($"Items[{i}].Producto");
                ModelState.Remove($"Items[{i}].FormaRegistro");
                ModelState.Remove($"Items[{i}].NumeroDocumento");
                ModelState.Remove($"Items[{i}].FechaCompra");
            }
        }


        // GET: InventarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InventarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InventarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InventarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<CreateRegistroInventarioViewModel> BuildCreateRegistroViewModelAsync()
        {
            var formasRegistro = await _context.FormaRegistro.ToListAsync();
            var proveedores = await _context.Proveedor.ToListAsync();
            var productos = await _context.Producto.ToListAsync();
            var ubicaciones = await _context.Ubicacion.ToListAsync();
            var estatus = await _context.Estatus.ToListAsync();
            var clasificaciones = await _context.Clasificacion.ToListAsync();

            var productoInfo = await _context.Producto
                .Include(p => p.IdDepartamentoNavigation)
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdLineaServicioNavigation)
                .Include(p => p.IdSubLineaServicioNavigation)
                .Select(p => new
                {
                    p.Codigo,
                    Departamento = p.IdDepartamentoNavigation.Nombre,
                    Categoria = p.IdCategoriaNavigation.Nombre,
                    LineaServicio = p.IdLineaServicioNavigation.Nombre,
                    SubLineaServicio = p.IdSubLineaServicioNavigation.Nombre
                    //SubLineaServicio = p.IdSubLineaServicioNavigation != null
                    //                   ? p.IdSubLineaServicioNavigation.Nombre
                    //                   : ""
                })
                .ToListAsync();

            return new CreateRegistroInventarioViewModel
            {
                FormasRegistroOptions = new SelectList(formasRegistro, "Id", "Nombre"),
                ProveedoresOptions = new SelectList(proveedores, "NombreComercial", "NombreComercial"),
                ProductosOptions = new SelectList(productos, "Codigo", "Nombre"),
                UbicacionesOptions = new SelectList(ubicaciones, "Nombre", "Nombre"),
                EstatusOptions = new SelectList(estatus, "Nombre", "Nombre"),
                ClasificacionesOptions = new SelectList(clasificaciones, "Nombre", "Nombre"),
                ProductInfoJson = JsonSerializer.Serialize(
                    productoInfo.ToDictionary(x => x.Codigo, x => new
                    {
                        x.Departamento,
                        x.Categoria,
                        x.LineaServicio,
                        x.SubLineaServicio
                    }),
                    new JsonSerializerOptions()
                )
            };
        }
    }
}
