using InventarioLPS.Data;
using InventarioLPS.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventarioLPS.Controllers
{
    public class ItemsInventarioController : Controller
    {
        private readonly InventarioLPSContext _context;

        public ItemsInventarioController(InventarioLPSContext context)
        {
            _context = context;
        }

        // GET: ItemsInventario
        public async Task<IActionResult> Index()
        {
            var inventarioLPSContext = _context.ItemInventario.Include(i => i.CodigoProductoNavigation)/*.Include(i => i.IdClasificacionNavigation)*/.Include(i => i.IdInformacionRegistroNavigation)/*.Include(i => i.IdProveedorNavigation).Include(i => i.IdUbicacionNavigation)*/;
            return View(await inventarioLPSContext.ToListAsync());
        }

        // GET: ItemsInventario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInventario = await _context.ItemInventario
                .Include(i => i.CodigoProductoNavigation)
                //.Include(i => i.IdClasificacionNavigation)
                .Include(i => i.IdInformacionRegistroNavigation)
                //.Include(i => i.IdProveedorNavigation)
                //.Include(i => i.IdUbicacionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemInventario == null)
            {
                return NotFound();
            }

            return View(itemInventario);
        }

        // GET: ItemsInventario/Create
        public IActionResult Create()
        {
            ViewData["FormasRegistro"] = new SelectList(_context.FormaRegistro, "Id", "Nombre");
            ViewData["CodigoProducto"] = new SelectList(_context.Producto, "Codigo", "Codigo");
            ViewData["IdClasificacion"] = new SelectList(_context.Clasificacion, "Id", "Nombre");
            ViewData["IdInformacionRegistro"] = new SelectList(_context.InformacionRegistro, "Id", "NumeroDocumento");
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Correo");
            ViewData["IdUbicacion"] = new SelectList(_context.Ubicacion, "Id", "Nombre");
            return View();
        }

        // POST: ItemsInventario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoItem,Departamento,Categoria,LineaServicio,SubLineaServicio,DescripcionEspecifica,EspecificacionesTecnicas,FechaCompra,ValorSinIva,NumeroParteFabricante,NumeroSerieLps,Estatus,IdUbicacion,IdClasificacion,CodigoProducto,IdProveedor,IdInformacionRegistro")] ItemInventario itemInventario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemInventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoProducto"] = new SelectList(_context.Producto, "Codigo", "Codigo", itemInventario.CodigoProducto);
            //ViewData["IdClasificacion"] = new SelectList(_context.Clasificacion, "Id", "Nombre", itemInventario.IdClasificacion);
            //ViewData["IdInformacionRegistro"] = new SelectList(_context.InformacionRegistro, "Id", "NumeroDocumento", itemInventario.IdInformacionRegistro);
            //ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Correo", itemInventario.IdProveedor);
            //ViewData["IdUbicacion"] = new SelectList(_context.Ubicacion, "Id", "Nombre", itemInventario.IdUbicacion);
            return View(itemInventario);
        }

        // GET: ItemsInventario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInventario = await _context.ItemInventario.FindAsync(id);
            if (itemInventario == null)
            {
                return NotFound();
            }
            ViewData["CodigoProducto"] = new SelectList(_context.Producto, "Codigo", "Codigo", itemInventario.CodigoProducto);
            //ViewData["IdClasificacion"] = new SelectList(_context.Clasificacion, "Id", "Nombre", itemInventario.IdClasificacion);
            //ViewData["IdInformacionRegistro"] = new SelectList(_context.InformacionRegistro, "Id", "NumeroDocumento", itemInventario.IdInformacionRegistro);
            //ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Correo", itemInventario.IdProveedor);
            //ViewData["IdUbicacion"] = new SelectList(_context.Ubicacion, "Id", "Nombre", itemInventario.IdUbicacion);
            return View(itemInventario);
        }

        // POST: ItemsInventario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoItem,Departamento,Categoria,LineaServicio,SubLineaServicio,DescripcionEspecifica,EspecificacionesTecnicas,FechaCompra,ValorSinIva,NumeroParteFabricante,NumeroSerieLps,Estatus,IdUbicacion,IdClasificacion,CodigoProducto,IdProveedor,IdInformacionRegistro")] ItemInventario itemInventario)
        {
            if (id != itemInventario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemInventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemInventarioExists(itemInventario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoProducto"] = new SelectList(_context.Producto, "Codigo", "Codigo", itemInventario.CodigoProducto);
            //ViewData["IdClasificacion"] = new SelectList(_context.Clasificacion, "Id", "Nombre", itemInventario.IdClasificacion);
            //ViewData["IdInformacionRegistro"] = new SelectList(_context.InformacionRegistro, "Id", "NumeroDocumento", itemInventario.IdInformacionRegistro);
            //ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Correo", itemInventario.IdProveedor);
            //ViewData["IdUbicacion"] = new SelectList(_context.Ubicacion, "Id", "Nombre", itemInventario.IdUbicacion);
            return View(itemInventario);
        }

        // GET: ItemsInventario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInventario = await _context.ItemInventario
                //.Include(i => i.CodigoProductoNavigation)
                //.Include(i => i.IdClasificacionNavigation)
                //.Include(i => i.IdInformacionRegistroNavigation)
                //.Include(i => i.IdProveedorNavigation)
                //.Include(i => i.IdUbicacionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemInventario == null)
            {
                return NotFound();
            }

            return View(itemInventario);
        }

        // POST: ItemsInventario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemInventario = await _context.ItemInventario.FindAsync(id);
            if (itemInventario != null)
            {
                _context.ItemInventario.Remove(itemInventario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemInventarioExists(int id)
        {
            return _context.ItemInventario.Any(e => e.Id == id);
        }
    }
}
