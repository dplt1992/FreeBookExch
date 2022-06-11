// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :::::: I N S T I T U T O :: P O L I T É C N I C O :: D E :: T O M A R ::::::
// ::::::::::::::::: E N G E N H A R I A :: I N F O R M Á T I C A :::::::::::::
// :::::::::::::: D E S E N V O L V I M E N T O :: W E B :: 2021/2022 :::::::::
// ::::::::::::::::::::::::::::::: Copyright(C) :::::::::::::::::::::::::::::::
// :::::::::: aluno19169@ipt.pt :::::::::::::: aluno21425@ipt.pt ::::::::::::::
// ::: https://github.com/dplt1992 https://github.com/Flavio-Oliveira-21425 :::
// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// ////////////////////////////////////////////////////////////////////////////

using FreeBooks.Data;
using FreeBooks.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FreeBooks.Controllers
{
    public class OfertasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfertasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ofertas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ofertas.Include(o => o.Anuncio).Include(o => o.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ofertas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ofertas == null)
            {
                return NotFound();
            }

            var ofertas = await _context.Ofertas
                .Include(o => o.Anuncio)
                .Include(o => o.Utilizador)
                .FirstOrDefaultAsync(m => m.IdOferta == id);
            if (ofertas == null)
            {
                return NotFound();
            }

            return View(ofertas);
        }

        // GET: Ofertas/Create
        public IActionResult Create()
        {
            ViewData["AnuncioFk"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio");
            ViewData["UtilizadorFk"] = new SelectList(_context.Utilizadores, "IdUser", "IdUser");
            return View();
        }

        // POST: Ofertas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOferta,Descricao,AnuncioFk,UtilizadorFk")] Ofertas ofertas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ofertas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnuncioFk"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", ofertas.AnuncioFk);
            ViewData["UtilizadorFk"] = new SelectList(_context.Utilizadores, "IdUser", "IdUser", ofertas.UtilizadorFk);
            return View(ofertas);
        }

        // GET: Ofertas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ofertas == null)
            {
                return NotFound();
            }

            var ofertas = await _context.Ofertas.FindAsync(id);
            if (ofertas == null)
            {
                return NotFound();
            }
            ViewData["AnuncioFk"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", ofertas.AnuncioFk);
            ViewData["UtilizadorFk"] = new SelectList(_context.Utilizadores, "IdUser", "IdUser", ofertas.UtilizadorFk);
            return View(ofertas);
        }

        // POST: Ofertas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOferta,Descricao,AnuncioFk,UtilizadorFk")] Ofertas ofertas)
        {
            if (id != ofertas.IdOferta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ofertas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfertasExists(ofertas.IdOferta))
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
            ViewData["AnuncioFk"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", ofertas.AnuncioFk);
            ViewData["UtilizadorFk"] = new SelectList(_context.Utilizadores, "IdUser", "IdUser", ofertas.UtilizadorFk);
            return View(ofertas);
        }

        // GET: Ofertas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ofertas == null)
            {
                return NotFound();
            }

            var ofertas = await _context.Ofertas
                .Include(o => o.Anuncio)
                .Include(o => o.Utilizador)
                .FirstOrDefaultAsync(m => m.IdOferta == id);
            if (ofertas == null)
            {
                return NotFound();
            }

            return View(ofertas);
        }

        // POST: Ofertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ofertas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ofertas'  is null.");
            }
            var ofertas = await _context.Ofertas.FindAsync(id);
            if (ofertas != null)
            {
                _context.Ofertas.Remove(ofertas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfertasExists(int id)
        {
            return _context.Ofertas.Any(e => e.IdOferta == id);
        }
    }
}
