using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Users
{
    public class UserInformationModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserInformationModel(
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
        private int userId = 0;
        public User userData { get; set; }
        public string Header = "Usuarios";

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : User {};

        private async Task LoadAsync(IdentityUser user)
        {
            if (Input == null)
            {
                userData = (User)_users.Get(_context, userId);

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
                    VatNumber = userData.VatNumber,
                    Deleted = userData.Deleted
                };
            }
        }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            #region Permission
            var permission = _user.GetUserDate(_context, user, PermissionAlias.User);
            if (!permission.Item1)
            {
                Notify(Header, "No posee permisos para esta opci�n", Models.Enum.NotificationType.warning);
                return RedirectToPage("Index");
            }

            Username = permission.Item2;  Picture = permission.Item3; CompanyName = permission.Item5;
            #endregion

            groups = new UserGroupController().GetList(_context);
            userId = id;
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
                    OnGetAsync(Input.Id);
                    return Page();
                }
                else
                {
                try
                {
                    if (string.IsNullOrEmpty(Input.FirstName) || string.IsNullOrEmpty(Input.LastName))
                    {
                        ModelState.AddModelError("ErrorMessage", "Nombre o Apellido no pueden estar vacio.");
                        await LoadAsync(user);
                        return Page();
                    }
                    else if (string.IsNullOrEmpty(Input.Email) || string.IsNullOrEmpty(Input.UserId))
                    {
                        ModelState.AddModelError("ErrorMessage", "Usuario o Correo no pueden estar vacio.");
                        await LoadAsync(user);
                        return Page();
                    }

                    User data = new User()
                    {

                        Id = Input.Id,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        Gender = Input.Gender,
                        Address = Input.Address,
                        CompanyId = Input.CompanyId,
                        GroupId = Input.GroupId,
                        UserId = Input.UserId,
                        Email = Input.Email,
                        MasterId = Input.MasterId,
                        Phone = Input.Phone,
                        VatNumber = Input.VatNumber,
                        Deleted = false
                    };

                    if (string.IsNullOrEmpty(Input.Address))
                        data.Address = " ";

                    if (Input.Id == 0)
                    {
                        var userCurrentData = (User)_users.Get(_context, user.Id);

                        data.CompanyId = userCurrentData.CompanyId;
                        data.MasterId = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        data.Deleted = Input.Deleted;
                    }

                    int value = _users.Save(_context, data, _userManager);

                    if (value <= 0) 
                    {
                        ModelState.AddModelError("ErrorMessage", "No se puedo almacenar la informacion del Usuario");
                        await LoadAsync(user);
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ErrorMessage", ex.Message);
                    await LoadAsync(user);
                    return Page();
                }

                Notify(Header);
                await _signInManager.RefreshSignInAsync(user);
                    return RedirectToPage("UserDetails");
                }
            
        }
    }
}
