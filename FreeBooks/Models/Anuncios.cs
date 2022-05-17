﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeBooks.Models
{
    public class Anuncios
    {
        public Anuncios()
        {
            Ofertas = new HashSet<Ofertas>();
            Livros = new HashSet<Livros>();
        }
        /// <summary>
        /// Chave Primaria do Anuncio
        /// </summary>
        [Key]
        [Display(Name = "Id Anuncio")]
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int IdAnuncio { get; set; }

        /// <summary>
        /// Titulo do Anuncio
        /// </summary>
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(50, ErrorMessage = "Não pode ter mais do que {1} caráteres.")]
        //[RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙa-zçáéíóúàèìòùãõäëïöüâêîôûñ '- ]+", ErrorMessage = "O {0} ter  menso do que {1} carateres.")]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        /// <summary>
        /// Tipo de Anuncio
        /// </summary>
        // Troca de Livro por Livro OU Venda do Livro
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(50, ErrorMessage = "Não pode ter mais do que {1} caráteres.")]
        //[RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙa-zçáéíóúàèìòùãõäëïöüâêîôûñ '- ]+", ErrorMessage = "O {0} ter  menso do que {1} carateres.")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        /// <summary>
        /// Descrição do Anuncio
        /// </summary>
        [StringLength(100, ErrorMessage = "Não pode ter mais do que {1} caráteres.")]
        //[RegularExpression("[A-ZÁÉÍÓÚÀÈÌÒÙa-zçáéíóúàèìòùãõäëïöüâêîôûñ '- ]+", ErrorMessage = "O {0} ter  menso do que {1} carateres.")]
        [Display(Name = "Descricao")]
        public string Descricao { get; set; }

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
        /// Data Expiracao / Fim  do Anuncio
        /// </summary>
        [Display(Name = "Data Final")]
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public DateTime DataExpiracao { get; set; }

        /// <summary>
        /// Ligação ao Utilizador que cria Anuncio
        /// </summary>
        [Display(Name = "Fk Utilizador")]
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFk { get; set; }
        public Utilizadores Utilizador { get; set; }

        /// <summary>
        /// Ligação ao Livro do Anunciado
        /// </summary>
        /*[Display(Name = "Fk Livro")]
        [ForeignKey(nameof(Livro))]
        public int LivroFk { get; set; }
        public Livros Livro { get; set; }*/

        /// <summary>
        /// Ligação a Transação do Anunciado
        /// </summary>
        [Display(Name = "Fk Transacao")]
        [ForeignKey(nameof(Transacao))]
        public int? TransacaoFk { get; set; }
        public Transacoes Transacao { get; set; }

        /// <summary>
        /// Lista de Ofertas ao Anuncio
        /// </summary>
        [Display(Name = "Lista de Ofertas")]
        public ICollection<Ofertas> Ofertas { get; set; }


        /// <summary>
        /// Lista de Livros Anunciados para troca
        /// </summary>
        [Display(Name = "Lista de Livros")]
        public ICollection<Livros> Livros { get; set; }

    }
}

