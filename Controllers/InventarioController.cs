using InventarioLPS.Data;
using InventarioLPS.Data.Entities;
using InventarioLPS.Models.Inventario;
using InventarioLPS.Services.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace InventarioLPS.Controllers
{
    public class InventarioController : Controller
    {
        private readonly InventarioLPSContext _context;

        public InventarioController(InventarioLPSContext context)
        {
            _context = context;

        }
        // GET: InventarioController
        public async Task<IActionResult> Index()
        {
            List<ItemInventario> inventario = await _context.ItemInventario
                .Include(i => i.IdInformacionRegistroNavigation.IdFormaRegistroNavigation)
                .Include(i => i.IdProveedorNavigation)
                .Include(i => i.CodigoProductoNavigation)
                .Include(i => i.IdUbicacionNavigation)
                .Include(i => i.IdClasificacionNavigation)
                .ToListAsync();

            var inventarioViewModel = InventarioMapping.ToViewModelList(inventario);

            return View(inventarioViewModel);
        }

        // GET: Inventario/Create
        public async Task<IActionResult> Create()
        {
            var model = await BuildCreateViewModelAsync();
            return View(model);
        }



        // POST: InventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRegistroInventarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var modelInvalid = await BuildCreateViewModelAsync();
                modelInvalid.Items = model.Items;
                return View(modelInvalid);
            }
            
            try
            {
                var newRegister = InventarioMapping.ToRegisterData(model);
                await _context.InformacionRegistro.AddAsync(newRegister);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error al guardar: " + ex.Message);
                return View(model);
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

        private async Task<CreateRegistroInventarioViewModel> BuildCreateViewModelAsync()
        {
            var formas = await _context.FormaRegistro.ToListAsync();
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
                .Select(p => new {
                    p.Codigo,
                    Departamento = p.IdDepartamentoNavigation.Nombre,
                    Categoria = p.IdCategoriaNavigation.Nombre,
                    LineaServicio = p.IdLineaServicioNavigation.Nombre,
                    SubLineaServicio = p.IdSubLineaServicioNavigation != null
                                       ? p.IdSubLineaServicioNavigation.Nombre
                                       : ""
                })
                .ToListAsync();

            return new CreateRegistroInventarioViewModel
            {
                FormasRegistroOptions = new SelectList(formas, "Id", "Nombre"),
                ProveedoresOptions = new SelectList(proveedores, "Id", "NombreComercial"),
                ProductosOptions = new SelectList(productos, "Codigo", "Nombre"),
                UbicacionesOptions = new SelectList(ubicaciones, "Id", "Nombre"),
                EstatusOptions = new SelectList(estatus, "Id", "Nombre"),
                ClasificacionesOptions = new SelectList(clasificaciones, "Id", "Nombre"),
                ProductInfoJson = JsonSerializer.Serialize(
                    productoInfo.ToDictionary(x => x.Codigo, x => new {
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
