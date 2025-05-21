using InventarioLPS.Data;
using InventarioLPS.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventarioLPS.Controllers
{
    public class SubLineasServicioController : Controller
    {
        private readonly InventarioLPSContext _context;

        public SubLineasServicioController(InventarioLPSContext context)
        {
            _context = context;
        }

        // GET: SubLineasServicio
        public async Task<IActionResult> Index()
        {
            var inventarioLPSContext = _context.SubLineaServicio.Include(s => s.IdLineaServicioNavigation);
            return View(await inventarioLPSContext.ToListAsync());
        }

        // GET: SubLineasServicio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLineaServicio = await _context.SubLineaServicio
                .Include(s => s.IdLineaServicioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subLineaServicio == null)
            {
                return NotFound();
            }

            return View(subLineaServicio);
        }

        // GET: SubLineasServicio/Create
        public IActionResult Create()
        {
            ViewData["IdLineaServicio"] = new SelectList(_context.LineaServicio, "Id", "Nombre");
            return View();
        }

        // POST: SubLineasServicio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,IdLineaServicio")] SubLineaServicio subLineaServicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subLineaServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLineaServicio"] = new SelectList(_context.LineaServicio, "Id", "Nombre", subLineaServicio.IdLineaServicio);
            return View(subLineaServicio);
        }

        // GET: SubLineasServicio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLineaServicio = await _context.SubLineaServicio.FindAsync(id);
            if (subLineaServicio == null)
            {
                return NotFound();
            }
            ViewData["IdLineaServicio"] = new SelectList(_context.LineaServicio, "Id", "Nombre", subLineaServicio.IdLineaServicio);
            return View(subLineaServicio);
        }

        // POST: SubLineasServicio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,IdLineaServicio")] SubLineaServicio subLineaServicio)
        {
            if (id != subLineaServicio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subLineaServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubLineaServicioExists(subLineaServicio.Id))
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
            ViewData["IdLineaServicio"] = new SelectList(_context.LineaServicio, "Id", "Nombre", subLineaServicio.IdLineaServicio);
            return View(subLineaServicio);
        }

        // GET: SubLineasServicio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLineaServicio = await _context.SubLineaServicio
                .Include(s => s.IdLineaServicioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subLineaServicio == null)
            {
                return NotFound();
            }

            return View(subLineaServicio);
        }

        // POST: SubLineasServicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subLineaServicio = await _context.SubLineaServicio.FindAsync(id);
            if (subLineaServicio != null)
            {
                _context.SubLineaServicio.Remove(subLineaServicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubLineaServicioExists(int id)
        {
            return _context.SubLineaServicio.Any(e => e.Id == id);
        }
    }
}
