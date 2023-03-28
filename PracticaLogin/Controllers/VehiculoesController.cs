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
    public class VehiculoesController : Controller
    {
        private readonly ContextDb _context;

        public VehiculoesController(ContextDb context)
        {
            _context = context;
        }

        // GET: Vehiculoes
        public async Task<IActionResult> Index()
        {
            var contextDb = _context.Vehiculos.Include(v => v.Conductor);
            return View(await contextDb.ToListAsync());
        }

        // GET: Vehiculoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Vehiculos == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Conductor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // GET: Vehiculoes/Create
        public IActionResult Create()
        {
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "id", "NombreCompleto");
            return View();
        }

        // POST: Vehiculoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Placa,Marca,Modelo,IdConductor")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                vehiculo.id = Guid.NewGuid();
                _context.Add(vehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "id", "NombreCompleto", vehiculo.IdConductor);
            return View(vehiculo);
        }

        // GET: Vehiculoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Vehiculos == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "id", "NombreCompleto", vehiculo.IdConductor);
            return View(vehiculo);
        }

        // POST: Vehiculoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Placa,Marca,Modelo,IdConductor")] Vehiculo vehiculo)
        {
            if (id != vehiculo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.id))
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
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "id", "NombreCompleto", vehiculo.IdConductor);
            return View(vehiculo);
        }

        // GET: Vehiculoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Vehiculos == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Conductor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Vehiculos == null)
            {
                return Problem("Entity set 'ContextDb.Vehiculos'  is null.");
            }
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(Guid id)
        {
          return (_context.Vehiculos?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
