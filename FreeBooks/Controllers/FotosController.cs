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
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FotosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Fotos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fotos.Include(f => f.Galeria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Fotos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fotos == null)
            {
                return NotFound();
            }

            var fotos = await _context.Fotos
                .Include(f => f.Galeria)
                .FirstOrDefaultAsync(m => m.IdFoto == id);
            if (fotos == null)
            {
                return NotFound();
            }

            return View(fotos);
        }

        // GET: Fotos/Create
        public IActionResult Create()
        {
            ViewData["GaleriaFk"] = new SelectList(_context.Galerias, "IdGaleria", "IdGaleria");
            return View();
        }

        // POST: Fotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFoto,Foto,GaleriaFk")] Fotos fotos, IFormFile newFoto)
        {
            if (newFoto == null)
            {
                fotos.Foto = "default.png";
            }
            else
            {
                if (!(newFoto.ContentType == "image/jpeg" || newFoto.ContentType == "image/png"))
                {
                    //write the error message
                    ModelState.AddModelError("", "If you want to send a file, chose an image");
                    //resend control to the view, with data provided by user
                    return View();
                }
                else
                {
                    //define image name
                    Guid g;
                    g = Guid.NewGuid();
                    string imageName = fotos.Foto + "_" + g.ToString();
                    string extensionOfImage = Path.GetExtension(newFoto.FileName).ToLower();
                    imageName += extensionOfImage;
                    //add image name to fotos data
                    fotos.Foto = imageName;
                }
            }

            //validate if data providaded by user is good...
            if (ModelState.IsValid)
            {
                try
                {
                    //add fotos data to database
                    _context.Add(fotos);
                    //commit
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    // if the code arrives here, someting wrong has appended
                    // we must fix the error or at least report it

                    //add a model error to our code
                    ModelState.AddModelError("", "something went rong. I can not store in the Database");
                    // eventually, before sending to the control view
                    // report error. For instance, write a message on disc
                    // or send an email to admin

                    // send control to View

                }
                //save image to file to disk
                //**************************
                if (newFoto != null)
                {
                    //ask the server what address it wants
                    string addressToStoreFile = _webHostEnvironment.WebRootPath;
                    string newImageLocalization = Path.Combine(addressToStoreFile, "Fotos", fotos.Foto);
                    //see if folder  exists
                    if (!Directory.Exists(newImageLocalization))
                    {
                        Directory.CreateDirectory(newImageLocalization);
                    }
                    //save image to file to disk
                    using var stream = new FileStream(newImageLocalization, FileMode.Create);
                    await newFoto.CopyToAsync(stream);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GaleriaFk"] = new SelectList(_context.Galerias, "IdGaleria", "IdGaleria", fotos.GaleriaFk);
            return View(fotos);
        }

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
            ViewData["GaleriaFk"] = new SelectList(_context.Galerias, "IdGaleria", "IdGaleria", fotos.GaleriaFk);
            return View(fotos);
        }

        // POST: Fotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFoto,Foto,GaleriaFk")] Fotos fotos, IFormFile newPhotoVet)
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
            ViewData["GaleriaFk"] = new SelectList(_context.Galerias, "IdGaleria", "IdGaleria", fotos.GaleriaFk);
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
                .Include(f => f.Galeria)
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
    }
}
