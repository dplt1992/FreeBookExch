using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FreeBooks.Data;
using FreeBooks.Models;

namespace FreeBooks.Controllers
{
    public class GaleriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GaleriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Galerias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Galerias.Include(g => g.Livro);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Galerias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Galerias == null)
            {
                return NotFound();
            }

            var galerias = await _context.Galerias
                .Include(g => g.Livro)
                .FirstOrDefaultAsync(m => m.IdGaleria == id);
            if (galerias == null)
            {
                return NotFound();
            }

            return View(galerias);
        }

        // GET: Galerias/Create
        public IActionResult Create()
        {
            ViewData["LivroFk"] = new SelectList(_context.Livros, "IdLivro", "IdLivro");
            return View();
        }

        // POST: Galerias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGaleria,LivroFk")] Galerias galerias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galerias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LivroFk"] = new SelectList(_context.Livros, "IdLivro", "IdLivro", galerias.LivroFk);
            return View(galerias);
        }

        // GET: Galerias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Galerias == null)
            {
                return NotFound();
            }

            var galerias = await _context.Galerias.FindAsync(id);
            if (galerias == null)
            {
                return NotFound();
            }
            ViewData["LivroFk"] = new SelectList(_context.Livros, "IdLivro", "IdLivro", galerias.LivroFk);
            return View(galerias);
        }

        // POST: Galerias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGaleria,LivroFk")] Galerias galerias)
        {
            if (id != galerias.IdGaleria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galerias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GaleriasExists(galerias.IdGaleria))
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
            ViewData["LivroFk"] = new SelectList(_context.Livros, "IdLivro", "IdLivro", galerias.LivroFk);
            return View(galerias);
        }

        // GET: Galerias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Galerias == null)
            {
                return NotFound();
            }

            var galerias = await _context.Galerias
                .Include(g => g.Livro)
                .FirstOrDefaultAsync(m => m.IdGaleria == id);
            if (galerias == null)
            {
                return NotFound();
            }

            return View(galerias);
        }

        // POST: Galerias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Galerias == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Galerias'  is null.");
            }
            var galerias = await _context.Galerias.FindAsync(id);
            if (galerias != null)
            {
                _context.Galerias.Remove(galerias);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GaleriasExists(int id)
        {
          return _context.Galerias.Any(e => e.IdGaleria == id);
        }
    }
}
