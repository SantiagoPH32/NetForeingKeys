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
    public class ConductorsController : Controller
    {
        private readonly ContextDb _context;

        public ConductorsController(ContextDb context)
        {
            _context = context;
        }

        // GET: Conductors
        public async Task<IActionResult> Index()
        {
            var contextDb = _context.Conductores.Include(c => c.Compania);
            return View(await contextDb.ToListAsync());
        }

        // GET: Conductors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Conductores == null)
            {
                return NotFound();
            }

            var conductor = await _context.Conductores
                .Include(c => c.Compania)
                .FirstOrDefaultAsync(m => m.id == id);
            if (conductor == null)
            {
                return NotFound();
            }

            return View(conductor);
        }

        // GET: Conductors/Create
        public IActionResult Create()
        {
            ViewData["IdCompania"] = new SelectList(_context.Companias, "id", "Nombre");
            return View();
        }

        // POST: Conductors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Documento,NombreCompleto,Correo,Celular,IdCompania")] Conductor conductor)
        {
            if (ModelState.IsValid)
            {
                conductor.id = Guid.NewGuid();
                _context.Add(conductor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompania"] = new SelectList(_context.Companias, "id", "Nombre", conductor.IdCompania);
            return View(conductor);
        }

        // GET: Conductors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Conductores == null)
            {
                return NotFound();
            }

            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null)
            {
                return NotFound();
            }
            ViewData["IdCompania"] = new SelectList(_context.Companias, "id", "Nombre", conductor.IdCompania);
            return View(conductor);
        }

        // POST: Conductors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Documento,NombreCompleto,Correo,Celular,IdCompania")] Conductor conductor)
        {
            if (id != conductor.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conductor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConductorExists(conductor.id))
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
            ViewData["IdCompania"] = new SelectList(_context.Companias, "id", "Nombre", conductor.IdCompania);
            return View(conductor);
        }

        // GET: Conductors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Conductores == null)
            {
                return NotFound();
            }

            var conductor = await _context.Conductores
                .Include(c => c.Compania)
                .FirstOrDefaultAsync(m => m.id == id);
            if (conductor == null)
            {
                return NotFound();
            }

            return View(conductor);
        }

        // POST: Conductors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Conductores == null)
            {
                return Problem("Entity set 'ContextDb.Conductores'  is null.");
            }
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor != null)
            {
                _context.Conductores.Remove(conductor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConductorExists(Guid id)
        {
          return (_context.Conductores?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
