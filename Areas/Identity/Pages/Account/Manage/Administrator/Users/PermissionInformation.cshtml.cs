using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Users
{
    public class PermissionInformationModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public PermissionInformationModel(
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
        private GroupPermissionController _permissionDetail = new GroupPermissionController();
        private int groupId = 0;
        public List<Permission> permissions { get; set; }
        public GroupPermission groupPermission { get; set; }
        public string Header = "Permisos";
        private List<string> groupPermissionToSave { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel 
        {
            public int GroupId { get; set; } = 0;
            public bool Main { get; set; } = false;
            public bool Company { get; set; } = false;
            public bool Affiliate { get; set; } = false;
            public bool Customer { get; set; } = false;
            public bool Fiscal { get; set; } = false;
            public bool Item { get; set; } = false;
            public bool Expenses { get; set; } = false;
            public bool User { get; set; } = false;
            public bool Profile { get; set; } = false;
            public bool Transaction { get; set; } = false;
            public bool Batch { get; set; } = false;
            public bool Report { get; set; } = false;

            public bool Account { get; set; } = false;
            public bool Income { get; set; } = false;
        };

        private async Task LoadAsync(IdentityUser user)
        {
            var list = _permissionDetail.GetList(_context, groupId);
            Input = new InputModel();
            Input.GroupId = groupId;
            Input.Affiliate = list.Any(a => a.PermissionId == PermissionAlias.Affiliate && !a.Deleted);
            Input.Batch = list.Any(a => a.PermissionId == PermissionAlias.Batch && !a.Deleted);
            Input.Company = list.Any(a => a.PermissionId == PermissionAlias.Company && !a.Deleted);
            Input.Customer = list.Any(a => a.PermissionId == PermissionAlias.Customer && !a.Deleted);
            Input.Expenses = list.Any(a => a.PermissionId == PermissionAlias.Expenses && !a.Deleted);
            Input.Fiscal = list.Any(a => a.PermissionId == PermissionAlias.Fiscal && !a.Deleted);
            Input.Item = list.Any(a => a.PermissionId == PermissionAlias.Item && !a.Deleted);
            Input.Main = list.Any(a => a.PermissionId == PermissionAlias.Main && !a.Deleted);
            Input.Profile = list.Any(a => a.PermissionId == PermissionAlias.Profile && !a.Deleted);
            Input.Report = list.Any(a => a.PermissionId == PermissionAlias.Report && !a.Deleted);
            Input.Transaction = list.Any(a => a.PermissionId == PermissionAlias.Transaction && !a.Deleted);
            Input.User = list.Any(a => a.PermissionId == PermissionAlias.User && !a.Deleted);
            Input.Account = list.Any(a => a.PermissionId == PermissionAlias.Account && !a.Deleted);
            Input.Income = list.Any(a => a.PermissionId == PermissionAlias.Income && !a.Deleted);
        }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
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

            groupId = id;
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

                    PermissionAdd();
                    var _userGroup = (UserGroup)new UserGroupController().Get(_context, Input.GroupId);

                    List<GroupPermission> line = new List<GroupPermission>();

                    foreach (var item in groupPermissionToSave)
                    {
                        GroupPermission data = new GroupPermission()
                        {
                            Id = 0,
                            PermissionId = item,
                            GroupId = _userGroup.GroupId,
                            Deleted = false
                        };
                        line.Add(data);
                    }
                    
                    _permissionDetail.Save(_context, line);
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
        private void PermissionAdd() 
        {
            groupPermissionToSave = new List<string>();
            if (Input.Affiliate)
                groupPermissionToSave.Add(PermissionAlias.Affiliate);
            if (Input.Batch)
                groupPermissionToSave.Add(PermissionAlias.Batch);
            if (Input.Company)
                groupPermissionToSave.Add(PermissionAlias.Company);
            if (Input.Customer)
                groupPermissionToSave.Add(PermissionAlias.Customer);
            if (Input.Expenses)
                groupPermissionToSave.Add(PermissionAlias.Expenses);
            if (Input.Fiscal)
                groupPermissionToSave.Add(PermissionAlias.Fiscal);
            if (Input.Item)
                groupPermissionToSave.Add(PermissionAlias.Item);
            if (Input.Main)
                groupPermissionToSave.Add(PermissionAlias.Main);
            if (Input.Profile)
                groupPermissionToSave.Add(PermissionAlias.Profile);
            if (Input.Report)
                groupPermissionToSave.Add(PermissionAlias.Report);
            if (Input.Transaction)
                groupPermissionToSave.Add(PermissionAlias.Transaction);
            if (Input.User)
                groupPermissionToSave.Add(PermissionAlias.User);
            if (Input.Account)
                groupPermissionToSave.Add(PermissionAlias.Account);
            if (Input.Income)
                groupPermissionToSave.Add(PermissionAlias.Income);
        }
    }
}
