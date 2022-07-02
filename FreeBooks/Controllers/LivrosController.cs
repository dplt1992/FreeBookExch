using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FreeBooks.Data;
using FreeBooks.Models;
using Microsoft.Extensions.FileProviders;

namespace FreeBooks.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public LivrosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
                .Include(a => a.Fotos)
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
        public async Task<IActionResult> Create([Bind("IdLivro,Titulo,Descricao,Edicao,Editora,Autor,AnuncioFK,OfertaFK")] Livros livros, LivrosViewModel fotoModel)
        {
            List<string> galeria = new List<string>();
            if (fotoModel.LivroFotos == null)
            {
                galeria.Add(getDefaultImg());
            }
            else
            {
                //Ler imagens e guarda nomes das imagens do livro
                galeria = await UploadFotos(fotoModel.LivroFotos);
            }

            if (ModelState.IsValid)
            {
                //Adicionar o livro a base de dados
                livros = addLivroToDB(livros.Titulo, livros.Descricao, livros.Edicao, livros.Editora, livros.Autor, livros.AnuncioFK, livros.OfertaFK);
                //salvar imagens na base de dados das Fotos
                List<Fotos> listaFotos = SaveFileToDB(livros,fotoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnuncioFK"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", livros.AnuncioFK);
            ViewData["OfertaFK"] = new SelectList(_context.Ofertas, "IdOferta", "IdOferta", livros.OfertaFK);
            return View(livros);
            ///return RedirectToAction("Index", "Fotos");
        }
        
        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }
            
            var fotos = await _context.Fotos
                .Where(foto=> foto.LivroFK == id)
                .ToListAsync();

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
        public async Task<IActionResult> Edit(int id, [Bind("IdLivro,Titulo,Descricao,Edicao,Editora,Autor,AnuncioFK,OfertaFK")] Livros livro, LivrosViewModel fotoModel)
        {
            if (id != livro.IdLivro || !LivrosExists(livro.IdLivro))
            {
                return NotFound();
            }
            //Lista com todas as Fotos do Livro
            List<string> allRootImages = getFotos(livro, fotoModel.LivroFotos);

            var LivrosIDPreviouslyStored = HttpContext.Session.GetInt32("IdLivro");

            if (LivrosIDPreviouslyStored == null)
            {
                // what we need to do?
                // we must decide...

                ModelState.AddModelError("", "You have spent more time than allowed...");
                return View(livro);
                // return RedirectToAction("Index");
            }

            if (LivrosIDPreviouslyStored != livro.IdLivro)
            {
                // if we enter here, something is wrong
                // what we need to do?????

                return RedirectToAction("Index");
            }
            //Ler nomes das imagens do livro
            List<string> galeria = await UploadFotos(fotoModel.LivroFotos);

            if (ModelState.IsValid) {
                //Adicionar o livro a base de dados
                livro = addLivroToDB(livro.Titulo, livro.Descricao, livro.Edicao, livro.Editora, livro.Autor, livro.AnuncioFK, livro.OfertaFK);
                //salvar imagens na base de dados das Fotos
                List<Fotos> listaFotos = SaveFileToDB(livro, fotoModel);
                await _context.SaveChangesAsync();
                
            }
            ViewData["AnuncioFK"] = new SelectList(_context.Anuncios, "IdAnuncio", "IdAnuncio", livro.AnuncioFK);
            ViewData["OfertaFK"] = new SelectList(_context.Ofertas, "IdOferta", "IdOferta", livro.OfertaFK);
            return View(livro);
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


        /************************/
        /************************/

        public Livros addLivroToDB(string titulo, string desc, int edicao, string editora, string autor, int? anuncio, int? oferta)
        {
            if (anuncio != null) {
                return new Livros
                {
                    Titulo = titulo,
                    Descricao = desc,
                    Edicao = edicao,
                    Editora = editora,
                    Autor = autor,
                    AnuncioFK  = anuncio
                };
            } else if(oferta != null) {
                return new Livros
                {
                    Titulo = titulo,
                    Descricao = desc,
                    Edicao = edicao,
                    Editora = editora,
                    Autor = autor,
                    OfertaFK = oferta
                };
            } else {
                return new Livros
                {
                    Titulo = titulo,
                    Descricao = desc,
                    Edicao = edicao,
                    Editora = editora,
                    Autor = autor
                };
            }
        }

        /// <summary>
        /// addFotoToDB
        /// Adiciona uma Foto a Tabela Fotos
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="livro"></param>
        /// <returns></returns>
        public Fotos addFotoToDB(string fName, Livros livro)
        {
            return new Fotos
            {
                Foto = fName,
                Livros = livro
            };
        }

        /// <summary>
        /// getFotoFileName
        /// Returns the name of the foto
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string getFotoFileName(IFormFile file, string name)
        {
            //define file name
            string fileName = name + "_" + Guid.NewGuid().ToString();
            string extensionOfFile = Path.GetExtension(file.FileName).ToLower();
            fileName += extensionOfFile;
            //return fileName
            return fileName;
        }
        /// <summary>
        /// fileType
        /// Valida o tipo de Ficheiro e retorna o nome do ficheiro, caso este seja valido. 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool fileType(IFormFile file)
        {
            if (file.ContentType.Equals("image/jpeg") || file.ContentType.Equals("image/png"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// upload image files
        /// le os ficheiros selecionados
        /// </summary>
        /// <param name="FormFile"></param>
        /// <returns></returns>
        public async Task<List<string>> UploadFotos(List<IFormFile> FormFile)
        {
            List<string> FotoFilesNames = new List<string>();
            foreach (var file in FormFile)
            {
                if (fileType(file) != true)
                {
                    FotoFilesNames.Add(getFotoFileName(file, file.FileName.ToString()));
                }
                if (file.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            return FotoFilesNames;
        }

        public List<string> getFotos(Livros livro, List<IFormFile> FormFile)
        {
            List<string> FotoFilesNames = OnGet();
            //Ler todos os ficheiros do tipo imagem
            foreach (var file in FormFile)
            {
                if (FotoFilesNames.Contains(".png") || FotoFilesNames.Contains(".jpg"))
                {
                    FotoFilesNames.Add(getFotoFileName(file, file.FileName.ToString()));
                }

            }
            //validar a lista das imagens relativamente ao id do Livro
            // e remover caso nao pertenção ao livro
            var fotosContext = _context.Fotos.Include(f => f.Livros);
            foreach (var foto in fotosContext)
            {
                if (foto.LivroFK != livro.IdLivro)
                {
                    FotoFilesNames.Remove(foto.Foto);
                }
            }
            
            return FotoFilesNames;
        }
        /******************/
        public List<string> OnGet()
        {
            List<string> ImageList = null;
            var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
            var contents = provider.GetDirectoryContents(Path.Combine("Fotos"));
            var objFiles = contents.OrderBy(m => m.LastModified);

            ImageList = new List<string>();
            foreach (var item in objFiles.ToList())
            {
                ImageList.Add(item.Name);
            }
            return ImageList;
        }
        /******************/

        /// <summary>
        /// Save files and uses addFileToBD
        /// Salvar imagens no wwwRoot
        /// adicionar Fotos na base de dados
        /// </summary>
        /// <param name="fotoModel"></param>
        /// <returns></returns>
        [HttpPost]
        public List<Fotos> SaveFileToDB(Livros livro, LivrosViewModel fotoModel)
        {
            List<Fotos> listaFotos = new List<Fotos>();
            //fotos.Foto = new List<string>();
            foreach (IFormFile img in fotoModel.LivroFotos)
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Fotos", img.FileName);
                var stream = new FileStream(path, FileMode.Create); //erro ir buscar caminho
                img.CopyToAsync(stream);
                Fotos auxFoto = addFotoToDB(img.FileName, livro);
                listaFotos.Add(auxFoto); 
            }
            _context.Fotos.AddRange(listaFotos);
            _context.SaveChanges();
            // não esquecer! Mandar msg para o user
            TempData["Mensagem"] = "Os ficheiros do tipo imagem foram adicionados com sucesso a galeria do Livro.";
            return listaFotos;
        }
        /************************/
        /************************/

        public ActionResult getFilesToEdit(Livros livro, LivrosViewModel fotoModel)
        {
            List<string> allRootImages = getFotos(livro, fotoModel.LivroFotos);

            ViewData["Message"] = allRootImages;
            return View();
        }

        public string getDefaultImg()
        {
            return "default" + Guid.NewGuid().ToString() + ".png";
        }
    }
}
