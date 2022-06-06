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
    public class TransacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transacoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transacoes.Include(t => t.Anuncios).Include(t => t.Ofertas);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transacoes == null)
            {
                return NotFound();
            }

            var transacoes = await _context.Transacoes
                .Include(t => t.Anuncios)
                .Include(t => t.Ofertas)
                .FirstOrDefaultAsync(m => m.IdTransacao == id);
            if (transacoes == null)
            {
                return NotFound();
            }

            return View(transacoes);
        }

        // GET: Transacoes/Create
        public IActionResult Create()
        {
            ViewData["AnuncioFK"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio");
            ViewData["OfertaFk"] = new SelectList(_context.Ofertas, "IdOferta", "IdOferta");
            return View();
        }

        // POST: Transacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTransacao,DataTrasancao,AnuncioFK,OfertaFk")] Transacoes transacoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transacoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnuncioFK"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", transacoes.AnuncioFK);
            ViewData["OfertaFk"] = new SelectList(_context.Ofertas, "IdOferta", "IdOferta", transacoes.OfertaFk);
            return View(transacoes);
        }

        // GET: Transacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transacoes == null)
            {
                return NotFound();
            }

            var transacoes = await _context.Transacoes.FindAsync(id);
            if (transacoes == null)
            {
                return NotFound();
            }
            ViewData["AnuncioFK"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", transacoes.AnuncioFK);
            ViewData["OfertaFk"] = new SelectList(_context.Ofertas, "IdOferta", "IdOferta", transacoes.OfertaFk);
            return View(transacoes);
        }

        // POST: Transacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransacao,DataTrasancao,AnuncioFK,OfertaFk")] Transacoes transacoes)
        {
            if (id != transacoes.IdTransacao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transacoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransacoesExists(transacoes.IdTransacao))
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
            ViewData["AnuncioFK"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", transacoes.AnuncioFK);
            ViewData["OfertaFk"] = new SelectList(_context.Ofertas, "IdOferta", "IdOferta", transacoes.OfertaFk);
            return View(transacoes);
        }

        // GET: Transacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transacoes == null)
            {
                return NotFound();
            }

            var transacoes = await _context.Transacoes
                .Include(t => t.Anuncios)
                .Include(t => t.Ofertas)
                .FirstOrDefaultAsync(m => m.IdTransacao == id);
            if (transacoes == null)
            {
                return NotFound();
            }

            return View(transacoes);
        }

        // POST: Transacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transacoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transacoes'  is null.");
            }
            var transacoes = await _context.Transacoes.FindAsync(id);
            if (transacoes != null)
            {
                _context.Transacoes.Remove(transacoes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransacoesExists(int id)
        {
          return _context.Transacoes.Any(e => e.IdTransacao == id);
        }
    }
}
