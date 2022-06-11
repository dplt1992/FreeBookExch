﻿// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
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
        /// Referência para a classe Livros
        /// </summary>
        //[Required]
        [Display(Name = "Fk Livros")]
        [ForeignKey(nameof(Livros))]
        public int LivroFK { get; set; }
        public Livros Livros { get; set; }
    }
}

