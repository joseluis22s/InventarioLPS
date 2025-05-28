using InventarioLPS.Data;
using InventarioLPS.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventarioLPS.Controllers
{
    public class ProductosController : Controller
    {
        private readonly InventarioLPSContext _context;

        public ProductosController(InventarioLPSContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var inventarioLPSContext = _context.Producto.Include(p => p.IdCategoriaNavigation).Include(p => p.IdDepartamentoNavigation).Include(p => p.IdLineaServicioNavigation).Include(p => p.IdSubLineaServicioNavigation);
            return View(await inventarioLPSContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdDepartamentoNavigation)
                .Include(p => p.IdLineaServicioNavigation)
                .Include(p => p.IdSubLineaServicioNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre");
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Nombre");
            ViewData["IdLineaServicio"] = new SelectList(_context.LineaServicio, "Id", "Nombre");
            ViewData["IdSubLineaServicio"] = new SelectList(_context.SubLineaServicio, "Id", "Nombre");
            ViewData["SublineasPorLinea"] = _context.SubLineaServicio
                .AsEnumerable()
                .GroupBy(s => s.IdLineaServicio)
                .ToDictionary(
                    g => g.Key.ToString(),
                    g => g.Select(s => new { id = s.Id, nombre = s.Nombre }).ToList());

            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nombre,IdDepartamento,IdCategoria,IdLineaServicio,IdSubLineaServicio")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Nombre", producto.IdDepartamento);
            ViewData["IdLineaServicio"] = new SelectList(_context.LineaServicio, "Id", "Nombre", producto.IdLineaServicio);
            ViewData["IdSubLineaServicio"] = new SelectList(_context.SubLineaServicio, "Id", "Nombre", producto.IdSubLineaServicio);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Nombre", producto.IdDepartamento);
            ViewData["IdLineaServicio"] = new SelectList(_context.LineaServicio, "Id", "Nombre", producto.IdLineaServicio);
            ViewData["IdSubLineaServicio"] = new SelectList(_context.SubLineaServicio, "Id", "Nombre", producto.IdSubLineaServicio);
            ViewData["SublineasPorLinea"] = _context.SubLineaServicio
                .AsEnumerable()
                .GroupBy(s => s.IdLineaServicio)
                .ToDictionary(
                    g => g.Key.ToString(),
                    g => g.Select(s => new { id = s.Id, nombre = s.Nombre }).ToList());
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Nombre,IdDepartamento,IdCategoria,IdLineaServicio,IdSubLineaServicio")] Producto producto)
        {
            if (id != producto.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Codigo))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Nombre", producto.IdDepartamento);
            ViewData["IdLineaServicio"] = new SelectList(_context.LineaServicio, "Id", "Nombre", producto.IdLineaServicio);
            ViewData["IdSubLineaServicio"] = new SelectList(_context.SubLineaServicio, "Id", "Nombre", producto.IdSubLineaServicio);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdDepartamentoNavigation)
                .Include(p => p.IdLineaServicioNavigation)
                .Include(p => p.IdSubLineaServicioNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(string id)
        {
            return _context.Producto.Any(e => e.Codigo == id);
        }
    }
}
