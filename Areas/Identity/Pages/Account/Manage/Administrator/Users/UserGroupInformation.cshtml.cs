using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Users
{
    public class UserGroupInformationModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserGroupInformationModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public string Username { get; set; } public string Picture { get; set; } public string CompanyName { get; set; }
        private UserMainController _user = new UserMainController();
        private UserGroupController _userGroup = new UserGroupController();
        private int userGroupId = 0;

        public UserGroup userGroup { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : UserGroup
        {
        };

        private async Task LoadAsync(IdentityUser user)
        {
            userGroup = (UserGroup)_userGroup.Get(_context, userGroupId);

            Input = new InputModel()
            {
                Id = userGroup.Id,
                CompanyId = userGroup.CompanyId,
                Deleted = userGroup.Deleted,
                GroupId = userGroup.GroupId,
                Name = userGroup.Name
            };
        }
        public string ErrorMessage { get; set; }
        public string Header = "Grupo de Usuarios";

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
                Notify(Header, "No posee permisos para esta opción", Models.Enum.NotificationType.warning);
                return RedirectToPage("Index");
            }

            Username = permission.Item2;  Picture = permission.Item3; CompanyName = permission.Item5;
            #endregion


            userGroupId = id;
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
                    LoadAsync(user);
                    return Page();
                }
                else
                {
                try
                {
                    if (string.IsNullOrEmpty(Input.Name))
                    {
                        ModelState.AddModelError("ErrorMessage", "Descripcion no pueden estar vacio.");
                        await LoadAsync(user);
                        return Page();
                    }

                    UserGroup data = new UserGroup()
                    {
                        Id = Input.Id,
                        CompanyId = Input.CompanyId,
                        Deleted = false,
                        GroupId = Input.GroupId,
                        Name = Input.Name
                    };

                    if (Input.Id == 0)
                    {
                        var comp = _user.Get(_context, user.Id);

                        data.CompanyId = comp.user.CompanyId;
                        data.GroupId = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        data.Deleted = Input.Deleted;
                    }

                    _userGroup.Save(_context, data);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ErrorMessage", ex.Message);
                    await LoadAsync(user);
                    return Page();
                }

                Notify(Header);
                await _signInManager.RefreshSignInAsync(user);
                    return RedirectToPage("UserGroupDetails");
                }
            
        }
    }
}
