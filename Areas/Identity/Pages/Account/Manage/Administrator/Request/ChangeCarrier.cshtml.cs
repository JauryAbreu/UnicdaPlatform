using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnicdaPlatform.Controllers.Request;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Request;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Request
{
    public class ChangeCareerModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public ChangeCareerModel(
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
        private RequestUserChangeCareerController _request = new RequestUserChangeCareerController();
        public List<RequestUserChangeCareer> requestList = new List<RequestUserChangeCareer>();
        public RequestUserChangeCareer request { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : RequestUserChangeCareer { };
        public string Header = "Cambio de Carrera";
        private async Task LoadAsync(int id, string userId)
        {
            request = (id > 0) ?(RequestUserChangeCareer)_request.Get(_context, id) : new RequestUserChangeCareer();

            if (!string.IsNullOrEmpty(userId))
                requestList = _request.GetList(_context, userId);

            if (Input == null)
            {
                Input = new InputModel()
                {
                    Id = request.Id,
                    UserId = request.UserId,
                    Comment = request.Comment,
                    ResponseComment = request.ResponseComment,
                    UserResponseId = request.UserResponseId,
                    Status = request.Status,
                    Deleted = request.Deleted
                };
            }
        }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, string userId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            #region Permission
            var permission = _user.GetUserDate(_context, user, PermissionAlias.Main);
            if (!permission.Item1)
            {
                Notify(Header, "No posee permisos para esta opción", Models.Enum.NotificationType.warning);
                return RedirectToPage("Index");
            }

            Username = permission.Item2;  Picture = permission.Item3; CompanyName = permission.Item5;
            #endregion

            await LoadAsync(id, userId);
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
                await OnGetAsync(Input.Id, Input.UserId);
                return Page();
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(Input.UserId) || string.IsNullOrEmpty(Input.Comment))
                    {
                        Notify(Header, "Cod. Usuario o Comentario no pueden estar vacio.", Models.Enum.NotificationType.warning);
                        await OnGetAsync(Input.Id, Input.UserId);
                        return Page();
                    }

                    var data = new RequestUserChangeCareer();

                    data.Id = Input.Id;
                    data.UserId = Input.UserId;
                    data.UserResponseId = Input.UserResponseId;
                    data.Comment = Input.Comment;
                    data.ResponseComment = Input.ResponseComment;
                    data.Status = Input.Status;
                    data.Deleted = false;

                    int value = _request.Save(_context, data);

                    if (value <= 0)
                    {
                        Notify(Header, "No se puedo almacenar la informacion del Cambio de Carrera", Models.Enum.NotificationType.warning);
                        await LoadAsync(Input.Id, Input.UserId);
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    Notify(Header, ex.Message, Models.Enum.NotificationType.error);
                    await OnGetAsync(Input.Id, Input.UserId);
                    return Page();
                }

                Notify(Header);
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage("Index");
            }

        }
    }
}
