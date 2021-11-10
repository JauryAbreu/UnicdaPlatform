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
using UnicdaPlatform.Controllers.Transactions;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
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
        public decimal ProfitAmount = 0;
        public decimal TotalSales = 0;
        public decimal CarAvailable = 0;
        public decimal CarExpiration = 0;
        public decimal DGIITotalAmount = 0;
        public decimal DGIINetAmount = 0;
        public decimal DGIIAmount = 0;
        public decimal ExpenseAmount = 0;
        public List<NcfHistory> ncfHistory { get; set; }
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

                if (DateFrom == DateTo)
                {
                    DateTime date = new DateTime(new Controllers.GenericFunctions().GetTimeZone().Year, 1, 1);
                    ncfHistory = _context.NcfHistory.Where(a => a.CreatedDate >= date).OrderByDescending(a => a.CreatedDate).ToList();

                    Input = new InputModel();
                    DateTo = Input.DateTo = new Controllers.GenericFunctions().GetTimeZone().Date.AddDays(1).AddSeconds(-1);
                    DateFrom = Input.DateFrom = date;

                }
                else
                {
                    ncfHistory = _context.NcfHistory.Where(a => a.CreatedDate >= DateFrom && a.CreatedDate <= DateTo).OrderByDescending(a => a.CreatedDate).ToList();

                    Input = new InputModel();
                    Input.DateTo = DateTo.Date.AddDays(1).AddSeconds(-1);
                    Input.DateFrom = DateFrom.Date;
                    
                }
                Input.Type = Type;

                foreach (var item in ncfHistory)
                {
                    DGIIAmount += item.TotalTax;
                    DGIINetAmount += item.TotalAmount;
                    DGIITotalAmount += item.TotalAmountWithTax;
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

            DateFrom = from;
            DateTo = to;
            Type = type;
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnGetGeneralPaymentsAsync()
        {
            var lines = new List<string>();

            string[] Values = new string[3];
            /*string[] Colors = { "#e62f2f", "#DA2F2F","#C22C2C","#B02828","#9B2525","#812121",
                                "#858796", "#7B7D8A","#676872","#53535A","#424248","#38383D",
                                "#4982d8", "#477AC7","#3F6CAF","#385D95","#325180","#29446C",
                                "#2c3763", "#334075","#3D4B83","#42549C","#586AB0","#26325F",
                                "#dc0402", "#DC0402", "#C10200","#A50200","#820200","#660200",
                                "#012c63", "#005DD4", "#0052BB","#004398","#003981","#002655" };*/

            int i = 1;
            int line = lines.Count;
            Random rnd = new Random();

            foreach (var item in lines)
            {
               /* Values[0] += (i != line) ? string.Format("{0};", item.Description) : item.Description;
                Values[1] += (i != line) ? string.Format("{0:0.00};", item.Amount) : string.Format("{0:0.00}", item.Amount);
                //Values[2] += (i != line) ? string.Format("{0};", Colors[rnd.Next(0, 35)]) : Colors[rnd.Next(0, 35)]);
                Values[2] += string.Format("{0};", (i == 1) ? "#e62f2f" : "#2c3763");
                i++;*/
            }
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
