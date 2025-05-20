using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventarioLPS.Data;
using InventarioLPS.Data.Entities;

namespace InventarioLPS.Controllers
{
    public class InformacionRegistrosController : Controller
    {
        private readonly InventarioLPSContext _context;

        public InformacionRegistrosController(InventarioLPSContext context)
        {
            _context = context;
        }

        // GET: InformacionRegistros
        public async Task<IActionResult> Index()
        {
            var inventarioLPSContext = _context.InformacionRegistro.Include(i => i.IdFormaRegistroNavigation);
            return View(await inventarioLPSContext.ToListAsync());
        }

        // GET: InformacionRegistros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacionRegistro = await _context.InformacionRegistro
                .Include(i => i.IdFormaRegistroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informacionRegistro == null)
            {
                return NotFound();
            }

            return View(informacionRegistro);
        }

        // GET: InformacionRegistros/Create
        public IActionResult Create()
        {
            ViewData["IdFormaRegistro"] = new SelectList(_context.FormaRegistro, "Id", "Nombre");
            return View();
        }

        // POST: InformacionRegistros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroDocumento,MontoTotal,IdFormaRegistro")] InformacionRegistro informacionRegistro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informacionRegistro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFormaRegistro"] = new SelectList(_context.FormaRegistro, "Id", "Nombre", informacionRegistro.IdFormaRegistro);
            return View(informacionRegistro);
        }

        // GET: InformacionRegistros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacionRegistro = await _context.InformacionRegistro.FindAsync(id);
            if (informacionRegistro == null)
            {
                return NotFound();
            }
            ViewData["IdFormaRegistro"] = new SelectList(_context.FormaRegistro, "Id", "Nombre", informacionRegistro.IdFormaRegistro);
            return View(informacionRegistro);
        }

        // POST: InformacionRegistros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroDocumento,MontoTotal,IdFormaRegistro")] InformacionRegistro informacionRegistro)
        {
            if (id != informacionRegistro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informacionRegistro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformacionRegistroExists(informacionRegistro.Id))
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
            ViewData["IdFormaRegistro"] = new SelectList(_context.FormaRegistro, "Id", "Nombre", informacionRegistro.IdFormaRegistro);
            return View(informacionRegistro);
        }

        // GET: InformacionRegistros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacionRegistro = await _context.InformacionRegistro
                .Include(i => i.IdFormaRegistroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informacionRegistro == null)
            {
                return NotFound();
            }

            return View(informacionRegistro);
        }

        // POST: InformacionRegistros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informacionRegistro = await _context.InformacionRegistro.FindAsync(id);
            if (informacionRegistro != null)
            {
                _context.InformacionRegistro.Remove(informacionRegistro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformacionRegistroExists(int id)
        {
            return _context.InformacionRegistro.Any(e => e.Id == id);
        }
    }
}
