using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Profile
{
    public class ProfileInformationModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public ProfileInformationModel(
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
        public class InputModel : User {};
        public string Header = "Usuario - Perfil";
        private async Task LoadAsync(IdentityUser user)
        {
            userData = (User)_users.Get(_context, user.Id);

            Input = new InputModel()
            {
                Id = userData.Id,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Gender = userData.Gender,
                Address = userData.Address,
                CompanyId = userData.CompanyId,
                GroupId = userData.GroupId,
                UserId = userData.UserId,
                Email = userData.Email,
                MasterId = userData.MasterId,
                Phone = userData.Phone,
                Deleted = userData.Deleted
            };
        }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
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

            await LoadAsync(user);
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
                await OnGetAsync();
                return Page();
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(Input.FirstName) || string.IsNullOrEmpty(Input.LastName))
                    {
                        Notify(Header, "Nombre o Apellido no pueden estar vacio.", Models.Enum.NotificationType.warning);
                        await OnGetAsync();
                        return Page();
                    }
                    else if (string.IsNullOrEmpty(Input.Email))
                    {
                        Notify(Header, "Correo no pueden estar vacio.", Models.Enum.NotificationType.warning);
                        await OnGetAsync();
                        return Page();
                    }

                    var data = (User)_users.Get(_context, user.Id);


                    data.FirstName = Input.FirstName;
                    data.LastName = Input.LastName;
                    data.Gender = Input.Gender;
                    data.Address = Input.Address;
                    data.Email = Input.Email;
                    data.Deleted = false;

                    int value = _users.Save(_context, data, _userManager);

                    if (value <= 0)
                    {
                        Notify(Header, "No se puedo almacenar la informacion del Usuario", Models.Enum.NotificationType.warning);
                        await LoadAsync(user);
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    Notify(Header, ex.Message, Models.Enum.NotificationType.error);
                    await OnGetAsync();
                    return Page();
                }

                Notify(Header);
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage("Index");
            }

        }
    }
}
