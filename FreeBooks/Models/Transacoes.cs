using System;
using System.ComponentModel.DataAnnotations;

namespace FreeBooks.Models
{
    public class Transacoes
    {
        public Transacoes()
        {
            Anuncios = new HashSet<Anuncios>();
            Ofertas = new HashSet<Ofertas>();
        }
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
        /// Lista do Anuncio
        /// </summary>
        [Display(Name = "Lista de Anuncios")]
        public ICollection<Anuncios> Anuncios { get; set; }

        /// <summary>
        /// Lista da Oferta
        /// </summary>
        [Display(Name = "Lista de Ofertas")]
        public ICollection<Ofertas> Ofertas { get; set; }

    }
}

