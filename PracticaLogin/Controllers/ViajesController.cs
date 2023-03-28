using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticaLogin.DTContext;
using PracticaLogin.Models;

namespace PracticaLogin.Controllers
{
    public class ViajesController : Controller
    {
        private readonly ContextDb _context;

        public ViajesController(ContextDb context)
        {
            _context = context;
        }

        // GET: Viajes
        public async Task<IActionResult> Index()
        {
            var contextDb = _context.Viajes.Include(v => v.Conductor);
            return View(await contextDb.ToListAsync());
        }

        // GET: Viajes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes
                .Include(v => v.Conductor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (viaje == null)
            {
                return NotFound();
            }

            return View(viaje);
        }

        // GET: Viajes/Create
        public IActionResult Create()
        {
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "id", "NombreCompleto");
            return View();
        }

        // POST: Viajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Fecha,Origen,Destino,Cliente,Producto,IdConductor")] Viaje viaje)
        {
            if (ModelState.IsValid)
            {
                viaje.id = Guid.NewGuid();
                _context.Add(viaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "id", "NombreCompleto", viaje.IdConductor);
            return View(viaje);
        }

        // GET: Viajes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "id", "NombreCompleto", viaje.IdConductor);
            return View(viaje);
        }

        // POST: Viajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Fecha,Origen,Destino,Cliente,Producto,IdConductor")] Viaje viaje)
        {
            if (id != viaje.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViajeExists(viaje.id))
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
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "id", "NombreCompleto", viaje.IdConductor);
            return View(viaje);
        }

        // GET: Viajes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes
                .Include(v => v.Conductor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (viaje == null)
            {
                return NotFound();
            }

            return View(viaje);
        }

        // POST: Viajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Viajes == null)
            {
                return Problem("Entity set 'ContextDb.Viajes'  is null.");
            }
            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje != null)
            {
                _context.Viajes.Remove(viaje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViajeExists(Guid id)
        {
          return (_context.Viajes?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
