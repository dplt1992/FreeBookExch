using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeBooks.Models
{
    public class Ofertas
    {
        /// <summary>
        /// Chave Primaria da Oferta
        /// </summary>
        [Key]
        [Display(Name = "Id Oferta")]
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int IdOferta { get; set; }

        /// <summary>
        /// Descrição a oferta
        /// </summary>
        [StringLength(100, ErrorMessage = "Não pode ter mais do que {1} caráteres.")]
        //[RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙa-zçáéíóúàèìòùãõäëïöüâêîôûñ '- ]+", ErrorMessage = "O {0} ter  menso do que {1} carateres.")]
        [Display(Name = "Nome")]
        public string Descricao { get; set; }

        /// <summary>
        /// Descrição a oferta
        /// </summary>
        [StringLength(100, ErrorMessage = "Não pode ter mais do que {1} caráteres.")]
        //[RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙa-zçáéíóúàèìòùãõäëïöüâêîôûñ '- ]+", ErrorMessage = "O {0} ter  menso do que {1} carateres.")]
        [Display(Name = "Montante")]
        public decimal Montante { get; set; }

        /// <summary>
        /// Estado do Anuncio
        /// </summary>
        [Display(Name = "Estado")]
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public bool Estado { get; set; }

        /// <summary>
        /// Data Lancamento / inicio do Anuncio
        /// </summary>
        [Display(Name = "Data Limite")]
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public DateTime DataLancamento { get; set; }

        /// <summary>
        /// Faz a ligação ao Anuncio ao qual foi realizada a Oferta
        /// </summary>
        [Display(Name = "Fk Anuncio")]
        [ForeignKey(nameof(Anuncio))]
        public int AnuncioFk { get; set; }
        public Anuncios Anuncio { get; set; }

        /// <summary>
        /// Faz a ligação ao Utilizador que criou a Oferta
        /// </summary>
        [Display(Name = "Fk Utilizador")]
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFk { get; set; }
        public Utilizadores Utilizador { get; set; }

        /// <summary>
        /// Lista de Livros que Poderam ser Trocados pelo do Anuncio
        /// </summary>
        [Display(Name = "Lista de Livros")]
        public ICollection<Livros> Livros { get; set; } = new HashSet<Livros>();

        /// <summary>
        /// Lista de Transações associadas a uma Oferta
        /// </summary>
        [Display(Name = "Lista de Transações")]
        public ICollection<Transacoes> Transacoes { get; set; } = new HashSet<Transacoes>();
    }
}

