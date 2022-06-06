using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeBooks.Models
{
    public class Fotos
    {
        /// <summary>
        /// Id da Foto
        /// </summary>
        [Key]
        //[Required]    
        [Display(Name = "Id Foto")]
        public int IdFoto { get; set; }

        /// <summary>
        /// Foto
        /// </summary>
        //[Required]
        [Display(Name = "Foto")]
        public string Foto { get; set; }

        /// <summary>
        /// Referência para a classe Livros
        /// </summary>
        //[Required]
        [Display(Name = "Fk Livros")]
        [ForeignKey(nameof(Livros))]
        public int LivroFK { get; set; }
        public Livros Livros { get; set; }
    }
}

