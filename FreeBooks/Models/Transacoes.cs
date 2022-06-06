using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeBooks.Models
{
    public class Transacoes
    {
        /// <summary>
        /// Chave Primaria da Transação
        /// </summary>
        [Key]
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int IdTransacao { get; set; }

        /// <summary>
        /// Data da Transação
        /// </summary>
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public DateTime DataTrasancao { get; set; }

        /// <summary>
        /// Referência para a classe Anuncio
        /// </summary>
        [Display(Name = "Fk Anuncio")]
        [ForeignKey(nameof(Anuncios))]
        public int? AnuncioFK { get; set; }
        public Anuncios Anuncios { get; set; }

        /// <summary>
        /// Referência para a classe Ofertas
        /// </summary>
        [Display(Name = "Fk Oferta")]
        [ForeignKey(nameof(Ofertas))]
        public int? OfertaFk { get; set; }
        public Ofertas Ofertas { get; set; }
    }
}

