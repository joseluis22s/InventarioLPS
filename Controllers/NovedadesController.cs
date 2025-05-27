using InventarioLPS.Data;
using InventarioLPS.Data.Entities;
using InventarioLPS.Models.NovedadesItem;
using InventarioLPS.Services.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioLPS.Controllers
{
    public class NovedadesController : BaseController
    {
        private readonly InventarioLPSContext _context;

        public NovedadesController(InventarioLPSContext context)
        {
            _context = context;

        }
        // GET: Novedades/Create/
        public async Task<IActionResult> Create(int idItem)
        {
            NovedadesViewModel model = await GetNovedadesByItemId(idItem);
            return View(model);
        }

        // POST: Novedades/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NovedadesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Existen campos no válidos.");
                return View(model);
            }
            try
            {
                NovedadItem newNovedad = NovedadesMapping.ToNovedadItem(model);
                // tod hacer algo con la persistencia de datos. de iteminvenatrio.
                await _context.NovedadItem.AddAsync(newNovedad);
                await _context.SaveChangesAsync();
                // Redirigir a la vista de detalles de la novedad
                return RedirectToAction(nameof(Details), new { idItem = model.IdItemInventario });
            }
            catch (Exception ex)
            {
                return ShowErrorPageFromException(ex);
            }
        }


        // GET: Novedades/Details/5
        public async Task<IActionResult> Details(int idItem)
        {
            NovedadesViewModel model = await GetNovedadesByItemId(idItem);
            return View(model);
        }

        private async Task<NovedadesViewModel> GetNovedadesByItemId(int idItem)
        {
            ItemInventario itemInvetario = await _context.ItemInventario
               .Include(i => i.CodigoProductoNavigation)
               .Include(i => i.IdInformacionRegistroNavigation.IdFormaRegistroNavigation)
               .Include(i => i.IdInformacionRegistroNavigation)
               .FirstAsync(i => i.Id == idItem);
            List<NovedadItem> novedadesItem = await _context.NovedadItem
                .Where(i => i.IdItemInventario == idItem)
                .ToListAsync();
            return NovedadesMapping.ToNovedadesItemViewModel(itemInvetario, novedadesItem);
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
            return RedirectToAction(nameof(Create), new { id });
        }
    }
}
