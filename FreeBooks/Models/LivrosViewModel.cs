using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeBooks.Models
{
    public class LivrosViewModel
    {
        /// <summary>
        /// Ficheiro da Foto
        /// </summary>
        public List<IFormFile> ListFilesFotos { get; set; }
    }
}

