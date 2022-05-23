using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeBooks.Models
{
    public class Galerias
    {
        public Galerias()
        {
            Fotos = new HashSet<Fotos>();
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
        /// Ligaçao ao livro ao qual pertence
        /// </summary>
        [Display(Name = "Fk Livro")]
        [ForeignKey(nameof(Livro))]
        public int LivroFk { get; set; }
        public Livros Livro { get; set; }

    }
}

