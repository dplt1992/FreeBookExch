using System;
using System.ComponentModel.DataAnnotations;

namespace FreeBooks.Models
{
    public class Utilizadores
    {
        public Utilizadores()
        {
            //Anuncios = new HashSet<Anuncios>();
            //Ofertas = new HashSet<Ofertas>();
        }

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
        /// Foto do Utilizador
        /// </summary>
        public string Foto { get; set; }

        /// <summary>
        /// Tipo de Utilizador
        /// </summary>
        //[Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string TipoUtilizador { get; set; }


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

