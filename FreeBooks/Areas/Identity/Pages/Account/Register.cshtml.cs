// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :::::: I N S T I T U T O :: P O L I T É C N I C O :: D E :: T O M A R ::::::
// ::::::::::::::::: E N G E N H A R I A :: I N F O R M Á T I C A :::::::::::::
// :::::::::::::: D E S E N V O L V I M E N T O :: W E B :: 2021/2022 :::::::::
// ::::::::::::::::::::::::::::::: Copyright(C) :::::::::::::::::::::::::::::::
// :::::::::: aluno19169@ipt.pt :::::::::::::: aluno21425@ipt.pt ::::::::::::::
// ::: https://github.com/dplt1992 https://github.com/Flavio-Oliveira-21425 :::
// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// ////////////////////////////////////////////////////////////////////////////

#nullable disable
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

using FreeBooks.Data;
using FreeBooks.Infrastructure;
using FreeBooks.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace FreeBooks.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        /*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        /// <summary>
        /// Representa o contexto de ligação à base de dados
        /// </summary>
        private readonly ApplicationDbContext _context;
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
        /*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
            ApplicationDbContext context,
            IUnitOfWork unitOfWork)
        /*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            /*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
            _context = context;
            _unitOfWork = unitOfWork;
            /*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        }

        /// <summary>
        /// Define os dados da interface
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Página para onde redirecionar o utilizador após o registo
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Métodos de autenticação externa
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// Define os dados que serão usados da interface
        /// </summary>
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            /*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
            /// <summary>
            /// Imagem da conta do Utilizador
            /// </summary>
            public string ImagePath { get; set; }

            /// <summary>
            /// Permite obter os dados do Utilizador para registo através da Identity
            /// </summary>
            public Utilizadores Utilizadores { get; set; }
            /*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        }

        /// <summary>
        /// Responde a pedidos HTTP GET
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }
        /// <summary>
        /// Responde a pedidos HTTP POST
        /// </summary>
        /// <param name="file">Ficheiro recebido do browser</param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(IFormFile file, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            // Uso para autenticação externa
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                ///*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
                _unitOfWork.SaveFile(file);

                //var user = new ApplicationUser
                //{
                //    UserName = Input.Email,
                //    Email = Input.Email,
                //    ImagePath = file.FileName
                //};
                ///*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/

                ApplicationUser user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                /* Dados do utilizador na tabela AspNetUsers */
                user.UserName = Input.Email;
                user.Email = Input.Email;
                user.Name = Input.Utilizadores.UserName;
                user.RegistrationDate = DateTime.Now;
                user.ImagePath = file.FileName;

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    /* Dados do utilizador na tabela Utilizadores */
                    // Foto do utilizador na tabela Utilizadores
                    Input.Utilizadores.Foto = file.FileName;
                    // Email do utilizador na tabela utilizadores
                    Input.Utilizadores.Email = Input.Email;
                    // Referencia do utilizador entre a tabela AspNetUsers e Utilizadores
                    Input.Utilizadores.UserId = user.Id;

                    try
                    {
                        // Prepara o contexto para registar o Utilizador
                        _context.Add(Input.Utilizadores);

                        // Regista efetivamente o Utilizador
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        // Algo correu mal. É possivel que o utilizador não foi registado na tabela Utilizadores
                        // Remover o utilizador da tabela AspNetUsers
                        await _userManager.DeleteAsync(user);

                        // Informar o utilizador do sucedido
                        ModelState.AddModelError("", "Não foi possivel, neste momento, registar o utilizador.");

                        // Passar o controlo à view de registo
                        return Page();
                    }

                    // Verificação do email
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
