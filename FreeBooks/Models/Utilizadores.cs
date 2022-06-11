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

namespace FreeBooks.Models
{
    public class Utilizadores
    {
        /// <summary>
        /// Chave Primaria do Utilizador
        /// </summary>
        [Key]
        [Display(Name = "Id do Utilizador")]
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int IdUser { get; set; }

        /// <summary>
        /// Nome do Utilizador
        /// </summary>
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(50, ErrorMessage = "Não pode ter mais do que {1} caráteres.")]
        //[RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙa-zçáéíóúàèìòùãõäëïöüâêîôûñ '- ]+", ErrorMessage = "O {0} ter  menso do que {1} carateres.")]
        [Display(Name = "Nome do Utilizador")]
        public string UserName { get; set; }

        /// <summary>
        /// Email do Utilizador
        /// </summary>
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Email { get; set; }

        /// <summary>
        /// Tipo de Utilizador
        /// </summary>
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string TipoUtilizador { get; set; }

        /// <summary>
        /// Foto do Utilizador
        /// </summary>
        public string Foto { get; set; }

        /// <summary>
        /// Referencia para a tabela AspNetUsers
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Lista de Anuncios do Utilizador
        /// </summary>
        [Display(Name = "Lista de Anucios")]
        public ICollection<Anuncios> Anuncios { get; set; }

        /// <summary>
        /// Lista de Ofertas a Anuncios do Utilizador
        /// </summary>
        [Display(Name = "Lista de Ofertas")]
        public ICollection<Ofertas> Ofertas { get; set; }

    }
}

