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
    public class AnunciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnunciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Anuncios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Anuncios.Include(a => a.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Anuncios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Anuncios == null)
            {
                return NotFound();
            }

            var anuncios = await _context.Anuncios
                .Include(a => a.Utilizador)
                .FirstOrDefaultAsync(m => m.IdAnuncio == id);
            if (anuncios == null)
            {
                return NotFound();
            }

            return View(anuncios);
        }

        // GET: Anuncios/Create
        public IActionResult Create()
        {
            ViewData["UtilizadorFk"] = new SelectList(_context.Utilizadores, "IdUser", "IdUser");
            return View();
        }

        // POST: Anuncios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAnuncio,Titulo,Tipo,Descricao,Estado,DataLancamento,DataExpiracao,UtilizadorFk")] Anuncios anuncios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anuncios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UtilizadorFk"] = new SelectList(_context.Utilizadores, "IdUser", "IdUser", anuncios.UtilizadorFk);
            return View(anuncios);
        }

        // GET: Anuncios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Anuncios == null)
            {
                return NotFound();
            }

            var anuncios = await _context.Anuncios.FindAsync(id);
            if (anuncios == null)
            {
                return NotFound();
            }
            ViewData["UtilizadorFk"] = new SelectList(_context.Utilizadores, "IdUser", "IdUser", anuncios.UtilizadorFk);
            return View(anuncios);
        }

        // POST: Anuncios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAnuncio,Titulo,Tipo,Descricao,Estado,DataLancamento,DataExpiracao,UtilizadorFk")] Anuncios anuncios)
        {
            if (id != anuncios.IdAnuncio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anuncios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnunciosExists(anuncios.IdAnuncio))
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
            ViewData["UtilizadorFk"] = new SelectList(_context.Utilizadores, "IdUser", "IdUser", anuncios.UtilizadorFk);
            return View(anuncios);
        }

        // GET: Anuncios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Anuncios == null)
            {
                return NotFound();
            }

            var anuncios = await _context.Anuncios
                .Include(a => a.Utilizador)
                .FirstOrDefaultAsync(m => m.IdAnuncio == id);
            if (anuncios == null)
            {
                return NotFound();
            }

            return View(anuncios);
        }

        // POST: Anuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Anuncios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Anuncios'  is null.");
            }
            var anuncios = await _context.Anuncios.FindAsync(id);
            if (anuncios != null)
            {
                _context.Anuncios.Remove(anuncios);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnunciosExists(int id)
        {
          return _context.Anuncios.Any(e => e.IdAnuncio == id);
        }
    }
}
