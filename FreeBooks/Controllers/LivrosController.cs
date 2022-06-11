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
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LivrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Livros.Include(l => l.Anuncio).Include(l => l.Oferta);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }

            var livros = await _context.Livros
                .Include(l => l.Anuncio)
                .Include(l => l.Oferta)
                .FirstOrDefaultAsync(m => m.IdLivro == id);
            if (livros == null)
            {
                return NotFound();
            }

            return View(livros);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            ViewData["AnuncioFK"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio");
            ViewData["OfertaFK"] = new SelectList(_context.Ofertas, "IdOferta", "IdOferta");
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLivro,Titulo,Descricao,Edicao,Editora,Autor,AnuncioFK,OfertaFK")] Livros livros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnuncioFK"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", livros.AnuncioFK);
            ViewData["OfertaFK"] = new SelectList(_context.Ofertas, "IdOferta", "IdOferta", livros.OfertaFK);
            return View(livros);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }

            var livros = await _context.Livros.FindAsync(id);
            if (livros == null)
            {
                return NotFound();
            }
            ViewData["AnuncioFK"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", livros.AnuncioFK);
            ViewData["OfertaFK"] = new SelectList(_context.Ofertas, "IdOferta", "IdOferta", livros.OfertaFK);
            return View(livros);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLivro,Titulo,Descricao,Edicao,Editora,Autor,AnuncioFK,OfertaFK")] Livros livros)
        {
            if (id != livros.IdLivro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivrosExists(livros.IdLivro))
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
            ViewData["AnuncioFK"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", livros.AnuncioFK);
            ViewData["OfertaFK"] = new SelectList(_context.Ofertas, "IdOferta", "IdOferta", livros.OfertaFK);
            return View(livros);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }

            var livros = await _context.Livros
                .Include(l => l.Anuncio)
                .Include(l => l.Oferta)
                .FirstOrDefaultAsync(m => m.IdLivro == id);
            if (livros == null)
            {
                return NotFound();
            }

            return View(livros);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Livros == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Livros'  is null.");
            }
            var livros = await _context.Livros.FindAsync(id);
            if (livros != null)
            {
                _context.Livros.Remove(livros);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivrosExists(int id)
        {
            return _context.Livros.Any(e => e.IdLivro == id);
        }
    }
}
