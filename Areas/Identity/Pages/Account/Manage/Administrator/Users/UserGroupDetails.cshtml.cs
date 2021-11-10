using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Users
{
    public class UserGroupDetailsModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserGroupDetailsModel(
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
        public List<UserGroup> userGroups { get; set; }
        public string Header = "Grupo de Usuarios";
        private async Task LoadAsync(IdentityUser user)
        {

            userGroups = _userGroup.GetList(_context);
        }

        public async Task<IActionResult> OnGetAsync()
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
            await LoadAsync(user);
            return Page();
        }

        //[HttpPost]
        public async Task<IActionResult> OnPostAsync(int groupId, int view, int delete)
        {

            if (view > 0)
            {
                return RedirectToPage("UserGroupInformation", new { id = view });
            }
            else if (groupId > 0) 
            {
                return RedirectToPage("PermissionInformation", new { id = groupId });
            }
            else if (delete > 0)
            {
                if (_context.UserGroup.Count(a => a.Id == delete) == 1)
                    _userGroup.Delete(_context, delete);
            }
            return RedirectToPage();
        }
    }
}
