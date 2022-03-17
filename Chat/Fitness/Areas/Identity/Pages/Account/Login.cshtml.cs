using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Fitness.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Fitness.Models.Repositries;
using Fitness.Models;

namespace Fitness.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> manager;
        private readonly IFitnessRepositry<Category> db;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, UserManager<ApplicationUser> _manager,IFitnessRepositry<Category> fitnessRepo)
        {
            _signInManager = signInManager;
            _logger = logger;
            manager = _manager;
            db = fitnessRepo;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //if (Request.Host.ToString().ToLower().Contains("folah.live"))
                //{
                ApplicationUser logingUser = await manager.FindByEmailAsync(Input.Email);
                if (logingUser != null && logingUser.userblocked==false)
                {
                    var result = await _signInManager.PasswordSignInAsync(logingUser.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        if (db.firstOrdefault(0,"EldahbDefault") == null)
                        {
                            Category eldhb = new Category()
                            { CatName= "EldahbDefault", ImgUrl=""};
                           await db.Add(eldhb);
                        }
                        _logger.LogInformation("User logged in.");
                        if (returnUrl.Length <= 2)
                        {
                            return Redirect("/Category/Index");
                        }
                        else
                            return LocalRedirect(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "بيانات الدخول غير صحيحه .  من فضلك حاول مره أخرى");
                    return Page();
                }
                //}
                //else
                //{
                //    return Redirect("https://wwww.facebook.com/hdtmahmoud");
                //}
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
