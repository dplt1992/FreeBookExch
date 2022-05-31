using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeBooks.Models
{
    public class FotosView
    {
      
        [Display(Name = "Foto")]
        public string Foto { get; set; }
        public List<IFormFile> ListaFotos { get; set; }
    }
}

