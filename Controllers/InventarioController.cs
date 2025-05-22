using InventarioLPS.Data;
using InventarioLPS.Models.Inventario;
using InventarioLPS.Services.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventarioLPS.Controllers
{
    public class InventarioController : Controller
    {
        private readonly InventarioLPSContext _context;
        private readonly SelectList _formasRegistro;
        private readonly SelectList _proveedores;
        private readonly SelectList _productos;
        private readonly SelectList _ubicaciones;
        private readonly SelectList _estatus;
        private readonly SelectList _clasificaciones;

        public InventarioController(InventarioLPSContext context)
        {
            _context = context;

            _formasRegistro = new SelectList(_context.FormaRegistro, "Id", "Nombre");
            _proveedores = new SelectList(_context.Proveedor, "Id", "NombreComercial");
            _productos = new SelectList(_context.Producto, "Codigo", "Nombre");
            _ubicaciones = new SelectList(_context.Ubicacion, "Id", "Nombre");
            _estatus = new SelectList(_context.Estatus, "Id", "Nombre");
            _clasificaciones = new SelectList(_context.Clasificacion, "Id", "Nombre");
        }
        // GET: InventarioController
        public async Task<IActionResult> Index()
        {
            var itemsInventario = await _context.ItemInventario
                .Include(i => i.IdInformacionRegistroNavigation)
                .Include(i => i.IdProveedorNavigation)
                .Include(i => i.CodigoProductoNavigation)
                .Include(i => i.IdUbicacionNavigation)
                .Include(i => i.IdClasificacionNavigation)
                .ToListAsync();

            var inventarioViewModel = InventarioMapping.ToViewModelList(itemsInventario);

            return View(inventarioViewModel);
        }

        // GET: InventarioController/Create
        public ActionResult Create()
        {
            LoadViewData();
            return View(new RegistroInventarioViewModel());
        }


        public async Task<IActionResult> GetNewItemRow(int index)
        {

            ViewData["Index"] = index;
            LoadViewData();
            return PartialView("_ItemInventarioPartial", new ItemInventarioViewModel());
            //try
            //{
            //    ViewData["Index"] = index;
            //    return PartialView("~/Views/Inventario/_ItemInventarioPartial.cshtml", new ItemInventarioViewModel());
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex, "Error al generar nueva fila por cantidad");
            //    return StatusCode(500, "Error al generar nueva fila por cantidad");
            //}
        }


        // POST: InventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistroInventarioViewModel model)
        {

            if (!ModelState.IsValid)
            {
                LoadViewData();
                return View(model);
            }
            try
            {
                //// 1. Guardar información del registro
                //var registro = new InformacionRegistro
                //{
                //    FormaRegistro = model.FormaRegistro,
                //    NumeroDocumento = model.NumeroDocumento,
                //    FechaCompra = model.FechaCompra,
                //    MontoTotal = model.MontoTotal
                //};

                //_context.Add(registro);
                //await _context.SaveChangesAsync();

                //// 2. Guardar items del inventario
                //foreach (var item in model.Items)
                //{
                //    var itemInventario = new ItemInventario
                //    {
                //        IdInformacionRegistro = registro.Id,
                //        CodigoItem = item.CodigoItem,
                //        Cantidad = item.Cantidad,
                //        ValorSinIva = item.ValorSinIva,
                //        IdProveedor = item.Proveedor,
                //        CodigoProducto = item.Producto,
                //        Departamento = item.Departamento,
                //        Categoria = item.Categoria,
                //        LineaServicio = item.LineaServicio,
                //        SubLineaServicio = item.SubLineaServicio,
                //        DescripcionEspecifica = item.DescripcionEspecifica,
                //        EspecificacionesTecnicas = item.EspecificacionesTecnicas,
                //        NumeroParteFabricante = item.NumeroParteFabricante,
                //        NumeroSerieLps = item.NumeroSerieLps,
                //        IdUbicacion = item.Ubicacion,
                //        IdEstatus = item.Estatus,
                //        IdClasificacion = item.Clasificacion
                //    };

                //    _context.Add(itemInventario);
                //}

                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error al guardar: " + ex.Message);
                LoadViewData();
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
        private void LoadViewData()
        {
            ViewData["FormasRegistro"] = _formasRegistro;
            ViewData["Proveedores"] = _proveedores;
            ViewData["Productos"] = _productos;
            ViewData["Ubicaciones"] = _ubicaciones;
            ViewData["Estatus"] = _estatus;
            ViewData["Clasificaciones"] = _clasificaciones;
        }
    }
}
