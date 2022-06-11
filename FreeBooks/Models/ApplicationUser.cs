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

using Microsoft.AspNetCore.Identity;

namespace FreeBooks.Models
{
    /// <summary>
    /// Representa os dados dos Utilizadores na tabela AspNetUsers
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Nome do utiliazador a ser usado na interface
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Date de registo do utilizador
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Imagem da conta para o Utilizador
        /// </summary>
        public string ImagePath { get; set; }
    }
}
