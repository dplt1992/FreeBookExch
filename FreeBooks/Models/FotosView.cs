using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeBooks.Models
{
    public class FotosView
    {
        /// <summary>
        /// Foto
        /// </summary>
        //[Required]
        [Display(Name = "Foto")]
        public string Foto { get; set; }
        public string[] ListaFotos { get; set; }
    }
}

