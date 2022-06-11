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
        [RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙa-zçáéíóúàèìòùãõäëïöüâêîôûñ '- ]+", ErrorMessage = "O {0} ter  menso do que {1} carateres.")]
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

