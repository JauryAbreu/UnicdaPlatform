using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator;
using UnicdaPlatform.Controllers.Transactions;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage

{
    [AllowAnonymous]
    public class IndexModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public string Username { get; set; }
        public string Picture { get; set; }
        public string CompanyName { get; set; }
        private UserMainController _user = new UserMainController();
        #region Main Data
        public string Header = "Inicio";
        #endregion

        private async Task LoadAsync(IdentityUser user)
        {
            try
            {
                var userName = await _userManager.GetUserNameAsync(user);
            }
            catch { }

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            #region Permission
            var permission = _user.GetUserDate(_context, user, PermissionAlias.Profile);

            Username = permission.Item2;
            Picture = permission.Item3; CompanyName = permission.Item5;
            CompanyName = permission.Item5;
            #endregion

            await LoadAsync(user);
            return Page();
        }
    }
}