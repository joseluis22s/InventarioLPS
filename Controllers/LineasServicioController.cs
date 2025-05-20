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
    public class LineasServicioController : Controller
    {
        private readonly InventarioLPSContext _context;

        public LineasServicioController(InventarioLPSContext context)
        {
            _context = context;
        }

        // GET: LineasServicio
        public async Task<IActionResult> Index()
        {
            return View(await _context.LineaServicio.ToListAsync());
        }

        // GET: LineasServicio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaServicio = await _context.LineaServicio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lineaServicio == null)
            {
                return NotFound();
            }

            return View(lineaServicio);
        }

        // GET: LineasServicio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LineasServicio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,TieneSubLinea")] LineaServicio lineaServicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lineaServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lineaServicio);
        }

        // GET: LineasServicio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaServicio = await _context.LineaServicio.FindAsync(id);
            if (lineaServicio == null)
            {
                return NotFound();
            }
            return View(lineaServicio);
        }

        // POST: LineasServicio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,TieneSubLinea")] LineaServicio lineaServicio)
        {
            if (id != lineaServicio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineaServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineaServicioExists(lineaServicio.Id))
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
            return View(lineaServicio);
        }

        // GET: LineasServicio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaServicio = await _context.LineaServicio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lineaServicio == null)
            {
                return NotFound();
            }

            return View(lineaServicio);
        }

        // POST: LineasServicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lineaServicio = await _context.LineaServicio.FindAsync(id);
            if (lineaServicio != null)
            {
                _context.LineaServicio.Remove(lineaServicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineaServicioExists(int id)
        {
            return _context.LineaServicio.Any(e => e.Id == id);
        }
    }
}
