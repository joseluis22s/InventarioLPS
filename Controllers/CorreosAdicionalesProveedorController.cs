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
    public class CorreosAdicionalesProveedorController : Controller
    {
        private readonly InventarioLPSContext _context;

        public CorreosAdicionalesProveedorController(InventarioLPSContext context)
        {
            _context = context;
        }

        // GET: CorreosAdicionalesProveedor
        public async Task<IActionResult> Index()
        {
            var inventarioLPSContext = _context.CorreoAdicionalProveedor.Include(c => c.IdProveedorNavigation);
            return View(await inventarioLPSContext.ToListAsync());
        }

        // GET: CorreosAdicionalesProveedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correoAdicionalProveedor = await _context.CorreoAdicionalProveedor
                .Include(c => c.IdProveedorNavigation)
                .FirstOrDefaultAsync(m => m.IdCorreo == id);
            if (correoAdicionalProveedor == null)
            {
                return NotFound();
            }

            return View(correoAdicionalProveedor);
        }

        // GET: CorreosAdicionalesProveedor/Create
        public IActionResult Create()
        {
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Correo");
            return View();
        }

        // POST: CorreosAdicionalesProveedor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCorreo,Correo,IdProveedor")] CorreoAdicionalProveedor correoAdicionalProveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correoAdicionalProveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Correo", correoAdicionalProveedor.IdProveedor);
            return View(correoAdicionalProveedor);
        }

        // GET: CorreosAdicionalesProveedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correoAdicionalProveedor = await _context.CorreoAdicionalProveedor.FindAsync(id);
            if (correoAdicionalProveedor == null)
            {
                return NotFound();
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Correo", correoAdicionalProveedor.IdProveedor);
            return View(correoAdicionalProveedor);
        }

        // POST: CorreosAdicionalesProveedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCorreo,Correo,IdProveedor")] CorreoAdicionalProveedor correoAdicionalProveedor)
        {
            if (id != correoAdicionalProveedor.IdCorreo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correoAdicionalProveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorreoAdicionalProveedorExists(correoAdicionalProveedor.IdCorreo))
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
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "Correo", correoAdicionalProveedor.IdProveedor);
            return View(correoAdicionalProveedor);
        }

        // GET: CorreosAdicionalesProveedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correoAdicionalProveedor = await _context.CorreoAdicionalProveedor
                .Include(c => c.IdProveedorNavigation)
                .FirstOrDefaultAsync(m => m.IdCorreo == id);
            if (correoAdicionalProveedor == null)
            {
                return NotFound();
            }

            return View(correoAdicionalProveedor);
        }

        // POST: CorreosAdicionalesProveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var correoAdicionalProveedor = await _context.CorreoAdicionalProveedor.FindAsync(id);
            if (correoAdicionalProveedor != null)
            {
                _context.CorreoAdicionalProveedor.Remove(correoAdicionalProveedor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorreoAdicionalProveedorExists(int id)
        {
            return _context.CorreoAdicionalProveedor.Any(e => e.IdCorreo == id);
        }
    }
}
