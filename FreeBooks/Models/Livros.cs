using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeBooks.Models
{
    public class Livros
    {
        [Key]
        [Display(Name = "Id Livro")]
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int IdLivro { get; set; }

        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(50, ErrorMessage = "Não pode ter mais do que {1} caráteres.")]
        //[RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙa-zçáéíóúàèìòùãõäëïöüâêîôûñ '- ]+", ErrorMessage = "O {0} ter  menso do que {1} carateres.")]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [StringLength(100, ErrorMessage = "Não pode ter mais do que {1} caráteres.")]
        //[RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙa-zçáéíóúàèìòùãõäëïöüâêîôûñ '- ]+", ErrorMessage = "O {0} ter  menso do que {1} carateres.")]
        [Display(Name = "Descricao")]
        public string Descricao { get; set; }

        [Display(Name = "Estado")]
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int Edicao { get; set; }


        /// <summary>
        /// Chave estrangeira do Anuncio relativo ao anuncio
        /// </summary>
        [Display(Name = "Editora")]
        public string Editora { get; set; }


        /// <summary>
        /// Chave estrangeira do Anuncio relativo ao anuncio
        /// </summary>
        [Display(Name = "Autor")]
        public string Autor { get; set; }

        /// <summary>
        /// Ligação ao Livro de um Anuncio caso exista
        /// </summary>
        [Display(Name = "Fk Anuncio")]
        [ForeignKey(nameof(Anuncio))]
        public int? AnuncioFK { get; set; }
        public Anuncios Anuncio { get; set; }

        /// <summary>
        /// Ligação ao Livro da Oferta a um Anuncio caso exista
        /// </summary>
        [Display(Name = "Fk Oferta")]
        [ForeignKey(nameof(Oferta))]
        public int? OfertaFK { get; set; }
        public Ofertas Oferta { get; set; }

        /// <summary>
        /// Lista de fotos associdas a um Livro
        /// </summary>
        [Display(Name = "Fotos do Livro")]
        public ICollection<Fotos> Fotos { get; set; }

    }
}

