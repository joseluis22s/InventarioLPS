using InventarioLPS.Data;
using InventarioLPS.Data.Entities;
using InventarioLPS.Models.NovedadesItem;
using InventarioLPS.Services.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventarioLPS.Controllers
{
    public class NovedadesItemController : Controller
    {
        private readonly InventarioLPSContext _context;

        public NovedadesItemController(InventarioLPSContext context)
        {
            _context = context;
        }

        // GET: NovedadesItem
        public async Task<IActionResult> Index()
        {
            var inventarioLPSContext = _context.NovedadItem.Include(n => n.IdItemInventarioNavigation);
            return View(await inventarioLPSContext.ToListAsync());
        }

        // GET: NovedadesItem/Details/5
        public async Task<IActionResult> Details(int idItem)
        {
            ItemInventario itemInvetario = await _context.ItemInventario
                .FirstAsync(i => i.Id == idItem);
            List<NovedadItem> novedadesItem = await _context.NovedadItem
                .Where(i => i.IdItemInventario == idItem)
                .ToListAsync();

            if (novedadesItem.Count == 0)
                return NotFound();

            NovedadesViewModel model = NovedadesMapping.ToNovedadesItemViewModel(itemInvetario, novedadesItem);

            return View(model);
        }

        // GET: NovedadesItem/Create
        public async Task<IActionResult> Create()
        {
            var model = new NovedadItem
            {
                Fecha = DateTime.Now
            };
            var itemsInventario = await _context.ItemInventario.Select(i => new { i.Id, i.CodigoItem }).ToListAsync();
            ViewData["IdItemInventario"] = new SelectList(itemsInventario, "Id", "CodigoItem");
            return View(model);
        }

        // POST: NovedadesItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Novedad,IdItemInventario")] NovedadItem novedadItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(novedadItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdItemInventario"] = new SelectList(_context.ItemInventario, "Id", "CodigoItem", novedadItem.IdItemInventario);
            return View(novedadItem);
        }

        // GET: NovedadesItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novedadItem = await _context.NovedadItem.FindAsync(id);
            if (novedadItem == null)
            {
                return NotFound();
            }
            ViewData["IdItemInventario"] = new SelectList(_context.ItemInventario, "Id", "Categoria", novedadItem.IdItemInventario);
            return View(novedadItem);
        }

        // POST: NovedadesItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Novedad,IdItemInventario")] NovedadItem novedadItem)
        {
            if (id != novedadItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(novedadItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NovedadItemExists(novedadItem.Id))
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
            ViewData["IdItemInventario"] = new SelectList(_context.ItemInventario, "Id", "Categoria", novedadItem.IdItemInventario);
            return View(novedadItem);
        }

        // GET: NovedadesItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var novedadItem = await _context.NovedadItem
                .Include(n => n.IdItemInventarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (novedadItem == null)
            {
                return NotFound();
            }

            return View(novedadItem);
        }

        // POST: NovedadesItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var novedadItem = await _context.NovedadItem.FindAsync(id);
            if (novedadItem != null)
            {
                _context.NovedadItem.Remove(novedadItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NovedadItemExists(int id)
        {
            return _context.NovedadItem.Any(e => e.Id == id);
        }
    }
}
