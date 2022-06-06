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
    public class FotosController : Controller
    {

        private string[] lista = null;
        private int counter = 0;

        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FotosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Fotos
        public async Task<IActionResult> Index(Fotos fotos, FotosView fotoView)
        {
            foreach (var newFoto in fotoView.ListaFotos)
            {
                fotos.Foto = getFotoName(fotos, newFoto);
            }
            var applicationDbContext = _context.Fotos.Include(f => f.Livro);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Fotos/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Fotos == null) {
                return NotFound();
            }
            var fotos = await _context.Fotos
                .Include(f => f.Livro)
                .FirstOrDefaultAsync(m => m.IdFoto == id);
            if (fotos == null)
            {
                return NotFound();
            }

            return View(fotos);
        }

        // GET: Fotos/Create
        public IActionResult Create() {
            ViewData["LivroFk"] = new SelectList(_context.Livros, "IdLivro", "IdLivro");
            return View();
        }

        // POST: Fotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFoto,Foto,LivroFK")] Fotos fotos, FotosView fotoView) {
            lista = null; counter = 0;
            //Validate Image
            foreach (var newFoto in fotoView.ListaFotos) { lista[counter] = getFotoName(fotos, newFoto); counter++; }
            //Gets the selected Livro FK from the view
            ViewData["LivroFk"] = new SelectList(_context.Livros, "IdLivro", "IdLivro", fotos.LivroFk);
            //validate if data providaded by user is good...
            if (ModelState.IsValid) {
                try {
                    //add fotos data to database
                    _context.Add(fotos);
                    //commit
                    await _context.SaveChangesAsync();
                } catch (Exception) {
                    // if the code arrives here Error, someting wrong has appended
                    ModelState.AddModelError("", "something went rong. I can not store in the Database");
                    // send control to View
                    return View();
                }
                if (fotoView.ListaFotos != null) {
                    lista = null; counter = 0;
                    foreach (var newFoto in fotoView.ListaFotos) {
                        if (lista[counter] != null)
                        {
                            //save image to file to disk
                            //**************************
                            FileToDir(newFoto, lista[counter]);
                            //Add Fotos To the Context Database
                            _context.Fotos.Add(AddFotos(lista[counter], (int)ViewData["LivroFk"]));
                        }
                        counter++;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fotos);
        }
        
        /*ImagesViewModel vm)
        {
            foreach (var item in vm.Images)
            {
                string fileName = UploadFile(item);

                var productImg = new Images
                {
                    ImgName = fileName,
                    Product = vm.Product
                };
                _context.Images.Add(productImg);
            }
            _context.SaveChanges();*/

        //old public async Task<IActionResult> Create([Bind("IdFoto,Foto,LivroFk")] Fotos fotos)
        /*
         public async Task<IActionResult> Create([Bind("IdFoto,Foto,LivroFk")] Fotos fotos)
        {

            if (ModelState.IsValid)
            {
                _context.Add(fotos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LivroFk"] = new SelectList(_context.Livros, "IdLivro", "IdLivro", fotos.LivroFk);
            return View(fotos);
        }
         */

        // GET: Fotos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fotos == null)
            {
                return NotFound();
            }

            var fotos = await _context.Fotos.FindAsync(id);
            if (fotos == null)
            {
                return NotFound();
            }
            ViewData["LivroFk"] = new SelectList(_context.Livros, "IdLivro", "IdLivro", fotos.LivroFk);
            return View(fotos);
        }

        // POST: Fotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFoto,Foto,LivroFk")] Fotos fotos)
        {
            if (id != fotos.IdFoto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotosExists(fotos.IdFoto))
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
            ViewData["LivroFk"] = new SelectList(_context.Livros, "IdLivro", "IdLivro", fotos.LivroFk);
            return View(fotos);
        }

        // GET: Fotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fotos == null)
            {
                return NotFound();
            }

            var fotos = await _context.Fotos
                .Include(f => f.Livro)
                .FirstOrDefaultAsync(m => m.IdFoto == id);
            if (fotos == null)
            {
                return NotFound();
            }

            return View(fotos);
        }

        // POST: Fotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fotos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Fotos'  is null.");
            }
            var fotos = await _context.Fotos.FindAsync(id);
            if (fotos != null)
            {
                _context.Fotos.Remove(fotos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FotosExists(int id)
        {
          return _context.Fotos.Any(e => e.IdFoto == id);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Add Fotos To the Context Database
        public Fotos AddFotos(string nFoto, int LivroFK) {
            return new Fotos { Foto = nFoto, LivroFk = LivroFK };
        }

        //save image to file to disk
        //**************************
        //Fuction to get the file stream of an image
        public void FileToDir(IFormFile nFoto, string img) {
            //ask the server what address it wants 
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Fotos", img);
            //see if folder  exists
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
            //save image to file to disk
            using var stream = new FileStream(path, FileMode.Create);
            nFoto.CopyToAsync(stream);
        }

        //Function to get the Foto
        public IFormFile getFoto(Fotos fotos, IFormFile nFoto)
        {
                //define image name
                string imgName = getFotoName(fotos, nFoto);
                //Upload Image
                FileToDir(nFoto, imgName);
                //add image name to fotos data
                return nFoto;
        }        

        //Function to get the Foto name
        public string getFotoName(Fotos fotos, IFormFile newFoto) {
            if (newFoto == null || !(newFoto.ContentType == "image/jpeg" || newFoto.ContentType == "image/png")) {
                return "default.png";
            } else {
                //define image name
                string imgName = fotos.Foto + "-" + fotos.IdFoto +"_" + Guid.NewGuid().ToString();
                string extensionOfImage = Path.GetExtension(newFoto.FileName).ToLower();
                imgName += extensionOfImage;
                //add image name to fotos data
                return imgName;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
