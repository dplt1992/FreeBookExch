using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeBooks.Models
{
    public class Ofertas
    {

        public Ofertas()
        {
            Livros = new HashSet<Livros>();
        }

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
        /// Faz a ligação a Transacao se esta existir
        /// </summary>
        [Display(Name = "Fk Transacao")]
        [ForeignKey(nameof(Transacao))]
        public int? TransacaoFk { get; set; }
        public Transacoes Transacao { get; set; }

        /// <summary>
        /// Lista de Livros que Poderam ser Trocados pelo do Anuncio
        /// </summary>
        [Display(Name = "Lista de Livros")]
        public ICollection<Livros> Livros { get; set; }

    }
}

