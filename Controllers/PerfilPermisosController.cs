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
    public class PerfilPermisosController : Controller
    {
        private readonly InventarioLPSContext _context;

        public PerfilPermisosController(InventarioLPSContext context)
        {
            _context = context;
        }

        // GET: PerfilPermisos
        public async Task<IActionResult> Index()
        {
            var inventarioLPSContext = _context.PerfilPermisos.Include(p => p.IdPerfilNavigation).Include(p => p.IdPermisoNavigation);
            return View(await inventarioLPSContext.ToListAsync());
        }

        // GET: PerfilPermisos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilPermisos = await _context.PerfilPermisos
                .Include(p => p.IdPerfilNavigation)
                .Include(p => p.IdPermisoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilPermisos == null)
            {
                return NotFound();
            }

            return View(perfilPermisos);
        }

        // GET: PerfilPermisos/Create
        public IActionResult Create()
        {
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "Id", "Nombre");
            ViewData["IdPermiso"] = new SelectList(_context.Permisos, "Id", "Nombre");
            return View();
        }

        // POST: PerfilPermisos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPermiso,IdPerfil")] PerfilPermisos perfilPermisos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfilPermisos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "Id", "Nombre", perfilPermisos.IdPerfil);
            ViewData["IdPermiso"] = new SelectList(_context.Permisos, "Id", "Nombre", perfilPermisos.IdPermiso);
            return View(perfilPermisos);
        }

        // GET: PerfilPermisos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilPermisos = await _context.PerfilPermisos.FindAsync(id);
            if (perfilPermisos == null)
            {
                return NotFound();
            }
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "Id", "Nombre", perfilPermisos.IdPerfil);
            ViewData["IdPermiso"] = new SelectList(_context.Permisos, "Id", "Nombre", perfilPermisos.IdPermiso);
            return View(perfilPermisos);
        }

        // POST: PerfilPermisos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPermiso,IdPerfil")] PerfilPermisos perfilPermisos)
        {
            if (id != perfilPermisos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfilPermisos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilPermisosExists(perfilPermisos.Id))
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
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "Id", "Nombre", perfilPermisos.IdPerfil);
            ViewData["IdPermiso"] = new SelectList(_context.Permisos, "Id", "Nombre", perfilPermisos.IdPermiso);
            return View(perfilPermisos);
        }

        // GET: PerfilPermisos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilPermisos = await _context.PerfilPermisos
                .Include(p => p.IdPerfilNavigation)
                .Include(p => p.IdPermisoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilPermisos == null)
            {
                return NotFound();
            }

            return View(perfilPermisos);
        }

        // POST: PerfilPermisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perfilPermisos = await _context.PerfilPermisos.FindAsync(id);
            if (perfilPermisos != null)
            {
                _context.PerfilPermisos.Remove(perfilPermisos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilPermisosExists(int id)
        {
            return _context.PerfilPermisos.Any(e => e.Id == id);
        }
    }
}
