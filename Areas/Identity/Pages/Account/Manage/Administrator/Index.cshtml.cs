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
using UnicdaPlatform.Controllers.CareerSubject;
using UnicdaPlatform.Controllers.Transactions;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CareerSubjects;
using UnicdaPlatform.Models.Fiscal;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator
{
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
        public int AmountInApp { get; set; }
        public double CustomerQty { get; set; }
        public static DateTime DateFrom { get; set; }
        public static DateTime DateTo { get; set; }
        public static int Type { get; set; }
        private UserMainController _user = new UserMainController();
        #region Main Data
        public decimal MatterInProgress = 0;
        public decimal MatterPending = 0;
        public decimal MatterTotal = 0;
        public decimal Average = 0;
        public List<MatterInProgress> matterInProgress { get; set; }
        public string Header = "Inicio";
        #endregion
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int Type { get; set; }
        };
        private async Task LoadAsync(IdentityUser user)
        {
            try
            {
                var userName = await _userManager.GetUserNameAsync(user);

                try
                {
                    var userData = await _userManager.GetUserAsync(User);
                    User _user = _context.User.First(a => a.MasterId == userData.Id);
                    List<CareerUserPensum> careerUserPensums = _context.CareerUserPensum.Where(a => a.UserId == _user.MasterId).ToList();
                    MatterPending = MatterInProgress - MatterTotal;

                    List<CareerPensum> careerPensums = _context.CareerPensum.Where(a => a.CareerId == _user.CareerId).ToList();
                    List<Matter> matters = new List<Matter>();
                    foreach (var item in careerPensums)
                    {
                        matters.Add(_context.Matter.First(a => a.MatterId == item.MatterId));
                    }

                    MatterInProgress = careerUserPensums.Where(a=>a.Status == 1).Count();
                    MatterTotal = matters.Count();
                    MatterPending = MatterTotal - careerUserPensums.Where(a => a.Status != 7 || a.Status != 8 || a.Status != 3).Count();

                    matterInProgress = new UserMatterController().GetMatterInProgress(_context, _user.UserId, 1);
                }
                catch (Exception)
                {
                }

            }
            catch { }

        }

        public async Task<IActionResult> OnGetAsync(DateTime from, DateTime to, int type = 3)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            #region Permission
            var permission = _user.GetUserDate(_context, user, PermissionAlias.Main);
            if (!permission.Item1)
                return RedirectToPage("/Account/Manage/Index");

            Username = permission.Item2;
            Picture = permission.Item3; CompanyName = permission.Item5;
            CompanyName = permission.Item5;
            #endregion

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnGetGeneralPaymentsAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            var pastelData = new TransactionController().GetPastelData(_context, user.Id);
            string[] Values = new string[3];

            Values[0] += string.Format("{0};{1};", "Creditos Cursados", "Total Creditos");
            Values[1] += string.Format("{0:0.00};{1:0.00};", pastelData.Item1, pastelData.Item2);
            Values[2] += string.Format("{0};{1};", "#5389DD","#83838E");

            return new JsonResult(Values);
        }

        public async Task<IActionResult> OnGetProfitAmountAsync()
        {
            string[] Values = new string[2];
            /* var lines = new TransactionController().GetProfitLines(_context, DateFrom, DateTo, 
                                                                     (Models.Transaction.Enum.TransactionReportType)Type).OrderBy(a=>a.CreatedDate).ToList();

             string[] Values = new string[2];
             int i = 1;
             int line = lines.Count;
             foreach (var item in lines)
             {
                 Values[0] += (i != line) ? string.Format("{0};", item.Description) : item.Description;
                 Values[1] += (i != line) ? string.Format("{0:0.00};", item.Amount) : string.Format("{0:0.00}", item.Amount);
                 i++;
             }*/
            return new JsonResult(Values);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage("Index", new { from = Input.DateFrom, to = Input.DateTo.AddDays(1), type = Input.Type });;
        }
    }
}
