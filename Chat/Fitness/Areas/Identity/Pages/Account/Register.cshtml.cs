using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Fitness.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Fitness.Models;
using Fitness.Models.Repositries;

namespace Fitness.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IFitnessRepositry<OtherLevel> otherLevelDb;
        private readonly IFitnessRepositry<Level> levelDb;
        private readonly IFitnessRepositry<ChargingLevel> chargingDb;
        private readonly RoleManager<IdentityRole> roleManger;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, 
            IFitnessRepositry<OtherLevel> otherLevelDb, IFitnessRepositry<Level> levelDb, IFitnessRepositry<ChargingLevel> ChargingDb, RoleManager<IdentityRole> roleManger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.otherLevelDb = otherLevelDb;
            this.levelDb = levelDb;
            chargingDb = ChargingDb;
            this.roleManger = roleManger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

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
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
               
                Level level = levelDb.firstOrdefault(0);
                OtherLevel other = otherLevelDb.firstOrdefault(0);
                ChargingLevel charging= chargingDb.firstOrdefault(0);
                int levelId = 0;
                int otherLevelId = 0;
                int chargingLevelId = 0;
                if (level != null)
                {
                    levelId = level.Id;
                }
                else
                {
                    level = new Level();
                    level.ImgUrl = "";
                    level.LevelName = "Zero level";
                    level.GiftSendCount = 0;
                    await levelDb.Add(level);
                    levelId = level.Id;
                }
                if (other != null)
                {
                    otherLevelId = other.Id;
                }
                else
                {
                    other = new OtherLevel();
                    other.GiftRecieverCount = 0;
                    other.ImgUrl = "";
                    other.LevelName = "Zero level";
                   await  otherLevelDb.Add(other);
                    otherLevelId = other.Id;
                }
                if (charging != null)
                {
                    chargingLevelId = charging.Id;
                }
                else
                {
                    charging = new ChargingLevel();
                    charging.BalanceCount = 0;
                    charging.ImgUrl = "";
                    charging.LevelName = "Zero level";
                    await chargingDb.Add(charging);
                    chargingLevelId = charging.Id;
                }
                var user = new ApplicationUser()
                {
                    UserName ="mahmoudYasein2022",
                    Email = Input.Email,
                    ImgUrl = "",
                    FulName = "Mahmoud yasein",
                    DateBirth = Convert.ToDateTime("01-07-1991"),
                    Gender =0,
                    LevelId = levelId,
                    OtherLevelId = otherLevelId,
                    ChargingLevelId=chargingLevelId,
                    UserId = 1000,
                    RegisterDate=DateTime.Now
                };
                IdentityResult result = await _userManager.CreateAsync(user, Input.Password);

                
                if (result.Succeeded)
                {
                    if (!await roleManger.RoleExistsAsync("Admin"))
                    {
                        IdentityRole _role = new IdentityRole() { Name = "Admin" };
                        await roleManger.CreateAsync(_role);
                    }
                    await _userManager.AddToRoleAsync(user, "Admin");

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
