using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnicdaPlatform.Controllers.CareerSubject;
using UnicdaPlatform.Controllers.Request;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CareerSubjects;
using UnicdaPlatform.Models.Request;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Request
{
    public class RemoveMatterModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RemoveMatterModel(
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
        private RequestUserMatterController _request = new RequestUserMatterController();

        UserMatterController _UserMatterController = new UserMatterController();
        public List<MatterInProgress> requestList = new List<MatterInProgress>();
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : RequestUserMatter 
        {
            public string Description { get; set; }
            public string MatterId { get; set; }
        };
        public string Header = "Retiro de Materia";
        private async Task LoadAsync(int id, string userId)
        {

            int period = _UserMatterController.GetCurrentPeriod();
            var matterInProgress = _UserMatterController.GetMatterInProgress(_context, userId, 1);
            var availableMatterToRemove = _UserMatterController.GetAvailableMatterToRemove(_context, userId);

            foreach (var item in matterInProgress)
            {
                if (!availableMatterToRemove.Exists(a => a.MatterId == item.MatterId && a.PeriodCycle == period && a.PeriodYear == DateTime.Now.Year))
                {
                    item.Description = string.Format("({0}) {1}", item.MatterId, item.Description);
                    requestList.Add(item);
                }
            }
        
            if (Input == null)
            {
                Input = new InputModel()
                {
                    Id = 0,
                    UserId = userId,
                    SessionCode = 1,
                    PeriodCycle = period,
                    PeriodYear = DateTime.Now.Year,
                    CareerPensumId = requestList.First().CareerPensumId,
                    Comment = string.Empty,
                    ResponseComment = string.Empty,
                    UserResponseId = string.Empty,
                    Description = string.Empty,
                    Status = 1,
                    Deleted = false
                };
            }
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
            var permission = _user.GetUserDate(_context, user, PermissionAlias.Main);
            if (!permission.Item1)
            {
                Notify(Header, "No posee permisos para esta opción", Models.Enum.NotificationType.warning);
                return RedirectToPage("Index");
            }

            Username = permission.Item2;  Picture = permission.Item3; CompanyName = permission.Item5;
            #endregion

            User _userData = _context.User.First(a => a.MasterId == user.Id);
            await LoadAsync(id, _userData.UserId);
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
                await OnGetAsync(Input.Id);
                return Page();
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(Input.UserId) || string.IsNullOrEmpty(Input.Comment))
                    {
                        Notify(Header, "Cod. Usuario o Comentario no pueden estar vacio.", Models.Enum.NotificationType.warning);
                        await OnGetAsync(Input.Id);
                        return Page();
                    }

                    var data = new RequestUserMatter();

                    User _userData = _context.User.First(a => a.MasterId == user.Id);
                    var matterInProgress = _UserMatterController.GetMatterInProgress(_context, _userData.UserId, 1);



                    data.Id = Input.Id;
                    data.UserId = user.Id;
                    data.PeriodYear = DateTime.Now.Year;
                    data.PeriodCycle = new UserMatterController().GetCurrentPeriod();
                    data.SessionCode = 1;
                    data.CareerPensumId = Input.CareerPensumId;
                    data.Comment = Input.Comment;
                    data.UserResponseId = string.Empty;
                    data.ResponseComment = string.Empty;
                    data.Status = 1;
                    data.Deleted = false;

                    int value = _request.Save(_context, data);

                    if (value <= 0)
                    {
                        Notify(Header, "No se puedo almacenar la informacion de retiro de materia", Models.Enum.NotificationType.warning);
                        await LoadAsync(Input.Id, Input.UserId);
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    Notify(Header, ex.Message, Models.Enum.NotificationType.error);
                    await OnGetAsync(Input.Id);
                    return Page();
                }

                Notify(Header);
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage("Index");
            }

        }
    }
}
