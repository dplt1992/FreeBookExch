using System;
using System.ComponentModel.DataAnnotations;

namespace FreeBooks.Models
{
    public class Galerias
    {
        public Galerias()
        {
            Fotos = new HashSet<Fotos>();
            Livros = new HashSet<Livros>();
        }


        /// <summary>
        /// Id da Galeria
        /// </summary>
        [Key]
        //[Required]
        [Display(Name = "Id Galeria")]
        public int IdGaleria { get; set; }


        /// <summary>
        /// Lista das Fotos contidas na Galeria
        /// </summary>
        [Display(Name = "Lista de Fotos")]
        public ICollection<Fotos> Fotos { get; set; }

        /// <summary>
        /// Lista dos Livros aos quais a Galeria pertence
        /// </summary>
        [Display(Name = "Lista de Livros")]
        public ICollection<Livros> Livros { get; set; }

    }
}

