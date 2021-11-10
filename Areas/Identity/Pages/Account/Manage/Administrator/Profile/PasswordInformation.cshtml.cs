using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Profile
{
    public class PasswordInformationModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public PasswordInformationModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public string Username { get; set; } public string Picture { get; set; } public string CompanyName { get; set; }
        public List<UserGroup> groups { get; set; }
        private UserMainController _user = new UserMainController();
        private UserController _users = new UserController();
        public User userData { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            public string ConfirmPassword { get; set; }
        }

        /*private async Task LoadAsync(IdentityUser user)
        {
        }*/
        public string Header = "Usuario - Contraseña";
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            #region Permission
            var permission = _user.GetUserDate(_context, user, PermissionAlias.Profile);
            if (!permission.Item1)
            {
                Notify(Header, "No posee permisos para esta opción", Models.Enum.NotificationType.warning);
                return RedirectToPage("Index");
            }

            Username = permission.Item2;  Picture = permission.Item3; CompanyName = permission.Item5;
            #endregion

            //await LoadAsync(user);
            return Page();
        }

        //[HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

                
                if (!ModelState.IsValid)
            {
                Notify(string.Format(Header, "Posee {0} campos con datos sin completar..."), ModelState.ErrorCount.ToString(), Models.Enum.NotificationType.warning);
                return Page();
                }
                else
                {
                try
                {
                    if (string.IsNullOrEmpty(Input.OldPassword))
                    {
                        Notify(Header, "Contraseña anterior no pueden estar vacio.", Models.Enum.NotificationType.warning);
                        return Page();
                    }
                    else if (string.IsNullOrEmpty(Input.NewPassword))
                    {
                        Notify(Header, "Contraseña nueva no pueden estar vacio.", Models.Enum.NotificationType.warning);
                        return Page();
                    }
                    else if (Input.NewPassword != Input.ConfirmPassword)
                    {
                        Notify(Header, "Las contraseña no coinciden.", Models.Enum.NotificationType.warning);
                        return Page();
                    }

                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword + "M@$t3r", Input.NewPassword + "M@$t3r");
                    if (!changePasswordResult.Succeeded)
                    {
                        foreach (var error in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    Notify(Header, ex.Message, Models.Enum.NotificationType.error);
                    return Page();
                }

                Notify(Header);
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage("Index");
                }
            
        }
    }
}
