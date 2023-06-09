﻿using System;
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
    public class CompaniasController : Controller
    {
        private readonly ContextDb _context;

        public CompaniasController(ContextDb context)
        {
            _context = context;
        }

        // GET: Companias
        public async Task<IActionResult> Index()
        {
              return _context.Companias != null ? 
                          View(await _context.Companias.ToListAsync()) :
                          Problem("Entity set 'ContextDb.Companias'  is null.");
        }

        // GET: Companias/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Companias == null)
            {
                return NotFound();
            }

            var compania = await _context.Companias
                .FirstOrDefaultAsync(m => m.id == id);
            if (compania == null)
            {
                return NotFound();
            }

            return View(compania);
        }

        // GET: Companias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,NIT,Nombre")] Compania compania)
        {
            if (ModelState.IsValid)
            {
                compania.id = Guid.NewGuid();
                _context.Add(compania);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compania);
        }

        // GET: Companias/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Companias == null)
            {
                return NotFound();
            }

            var compania = await _context.Companias.FindAsync(id);
            if (compania == null)
            {
                return NotFound();
            }
            return View(compania);
        }

        // POST: Companias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,NIT,Nombre")] Compania compania)
        {
            if (id != compania.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compania);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompaniaExists(compania.id))
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
            return View(compania);
        }

        // GET: Companias/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Companias == null)
            {
                return NotFound();
            }

            var compania = await _context.Companias
                .FirstOrDefaultAsync(m => m.id == id);
            if (compania == null)
            {
                return NotFound();
            }

            return View(compania);
        }

        // POST: Companias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Companias == null)
            {
                return Problem("Entity set 'ContextDb.Companias'  is null.");
            }
            var compania = await _context.Companias.FindAsync(id);
            if (compania != null)
            {
                _context.Companias.Remove(compania);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompaniaExists(Guid id)
        {
          return (_context.Companias?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
