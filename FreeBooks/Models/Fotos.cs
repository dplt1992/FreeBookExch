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
        /// Ligação da Foto com a Galeria a que pertence
        /// </summary>
        //[Required]
        [Display(Name = "Fk Galeria")]
        [ForeignKey(nameof(Galeria))]
        public int GaleriaFk { get; set; }
        public Galerias Galeria { get; set; }
    }
}

