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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id Foto")]
        public int IdFoto { get; set; }

        /// <summary>
        /// Foto
        /// </summary>
        //[Required]
        [Display(Name = "Foto")]
        [FileExtensions()]
        public string Foto { get; set; }

        /// <summary>
        /// Ligação da Foto com a Foto a que pertence
        /// </summary>
        //[Required]
        [Display(Name = "Fk Livro")]
        [ForeignKey(nameof(Livro))]
        public int LivroFk { get; set; }
        public Livros Livro { get; set; }
    }
}

