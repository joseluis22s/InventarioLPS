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

        public InventarioController(InventarioLPSContext context)
        {
            _context = context;
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

        // GET: InventarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InventarioController/Create
        public ActionResult Create()
        {
            ViewData["FormasRegistro"] = new SelectList(_context.FormaRegistro, "Id", "Nombre");
            ViewData["Proveedores"] = new SelectList(_context.Proveedor, "Id", "NombreComercial");
            ViewData["Productos"] = new SelectList(_context.Producto, "Codigo", "Nombre");
            ViewData["Ubicaciones"] = new SelectList(_context.Ubicacion, "Id", "Nombre");
            ViewData["Estatus"] = new SelectList(_context.Estatus, "Id", "Nombre");
            ViewData["Clasificaciones"] = new SelectList(_context.Clasificacion, "Id", "Nombre");
            return View(new RegistroInventarioViewModel());
        }


        public async Task<IActionResult> GetNewRow(int index)
        {
            try
            {
                ViewData["Index"] = index;
                return PartialView("~/Views/Inventario/_ClonarFilaPartial.cshtml", new ItemInventarioClonViewModel());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex, "Error al generar nueva fila por cantidad");
                return StatusCode(500, "Error al generar nueva fila por cantidad");
            }
        }
        public IActionResult GetNewRowDuplicado(int index)
        {

            try
            {
                ViewData["Index"] = index;
                ViewData["MostrarBotonDuplicar"] = true;
                return PartialView("~/Views/Inventario/_ClonarFilaPartial.cshtml", new ItemInventarioClonViewModel());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex, "Error al generar nueva fila para duplicar");
                return StatusCode(500, "Error al generar nueva fila para duplicar");
            }
        }

        // POST: InventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
    }
}
