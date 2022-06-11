// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :::::: I N S T I T U T O :: P O L I T É C N I C O :: D E :: T O M A R ::::::
// ::::::::::::::::: E N G E N H A R I A :: I N F O R M Á T I C A :::::::::::::
// :::::::::::::: D E S E N V O L V I M E N T O :: W E B :: 2021/2022 :::::::::
// ::::::::::::::::::::::::::::::: Copyright(C) :::::::::::::::::::::::::::::::
// :::::::::: aluno19169@ipt.pt :::::::::::::: aluno21425@ipt.pt ::::::::::::::
// ::: https://github.com/dplt1992 https://github.com/Flavio-Oliveira-21425 :::
// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// ////////////////////////////////////////////////////////////////////////////

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

