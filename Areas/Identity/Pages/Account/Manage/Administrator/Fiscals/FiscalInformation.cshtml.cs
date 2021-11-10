using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnicdaPlatform.Controllers.Fiscal;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Fiscal;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Fiscals
{
    public class FiscalInformationModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public FiscalInformationModel(
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
        private NcfSequenceDetailController _ncfSequenceDetail = new NcfSequenceDetailController();
        private int ncfSequenceDetailId = 0;
        private int ncfTypeId = 0;
        public string Header = "Información Fiscal";
        public NcfSequenceDetail ncfSequenceDetail { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : NcfSequenceDetail 
        {
            public string NcfDescription { get; set; }
        };

        private async Task LoadAsync(IdentityUser user)
        {
            ncfSequenceDetail = (NcfSequenceDetail)_ncfSequenceDetail.Get(_context, ncfSequenceDetailId);

            Input = new InputModel()
            {
                Id = ncfSequenceDetail.Id,
                NcfId = ncfSequenceDetail.NcfId,
                DateEnd = ncfSequenceDetail.DateEnd,
                DateStart = ncfSequenceDetail.DateStart,
                DGIIDescription = ncfSequenceDetail.DGIIDescription,
                CompanyId = ncfSequenceDetail.CompanyId,
                SeqEnd = ncfSequenceDetail.SeqEnd,
                SeqNext = ncfSequenceDetail.SeqNext,
                SeqStart = ncfSequenceDetail.SeqStart,
                SeqStatus = ncfSequenceDetail.SeqStatus,
                Serie = ncfSequenceDetail.Serie,
                MasterId = ncfSequenceDetail.MasterId,
                Deleted = ncfSequenceDetail.Deleted
            };

            GetNcfName();
        }

        private void GetNcfName() 
        {
            if (Input.NcfId == 0)
            {
                Input.NcfId = ncfTypeId;
                Input.DateStart = new Controllers.GenericFunctions().GetTimeZone().Date;
                Input.DateEnd = new DateTime(new Controllers.GenericFunctions().GetTimeZone().Year + 2, 1, 1).AddDays(-1);
            }
            switch (Input.NcfId) 
            {
                case 1:
                    Input.NcfDescription = "Credito Fiscal";
                    
                    break;
                case 2:
                    Input.NcfDescription = "Consumidor Fiscal";
                    break;
                case 3:
                    Input.NcfDescription = "Nota de Debito";
                    break;
                case 4:
                    Input.NcfDescription = "Nota de Credito";
                    break;
                case 14:
                    Input.NcfDescription = "Regimen Especial";
                    break;
                case 15:
                    Input.NcfDescription = "Gubernamental";
                    break;
            }
        }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id = 0, int ncfType = 2)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            #region Permission
            var permission = _user.GetUserDate(_context, user, PermissionAlias.Fiscal);
            if (!permission.Item1)
            {
                Notify(Header, "No posee permisos para esta opción", Models.Enum.NotificationType.warning);
                return RedirectToPage("Index");
            }

            Username = permission.Item2;  Picture = permission.Item3; CompanyName = permission.Item5;
            #endregion

            ncfSequenceDetailId = id;
            ncfTypeId = ncfType;
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
                await OnGetAsync(Input.NcfId, Input.Id);
                return Page();
            }
            else
            {
                try
                {
                    if (Input.SeqStart < 0 || Input.SeqNext < Input.SeqStart || Input.SeqNext > Input.SeqEnd)
                    {
                        Notify(Header, "La numeracion de secuencia no es valida", Models.Enum.NotificationType.warning);
                        await OnGetAsync(Input.NcfId, Input.Id);
                        return Page();
                    }
                    else if (Input.DateStart < new Controllers.GenericFunctions().GetTimeZone().Date.AddYears(-2))
                    {
                        Notify(Header, "Fecha de Inicio no es valida", Models.Enum.NotificationType.warning);
                        await OnGetAsync(Input.NcfId, Input.Id);
                        return Page();
                    }
                    else if (string.IsNullOrEmpty(Input.DGIIDescription) || string.IsNullOrEmpty(Input.Serie))
                    {
                        Notify(Header, "Serie o Descripcion no pueden estar vacio", Models.Enum.NotificationType.warning);
                        await OnGetAsync(Input.NcfId, Input.Id);
                        return Page();
                    }

                    NcfSequenceDetail data = new NcfSequenceDetail()
                    {
                        Id = Input.Id,
                        NcfId = Input.NcfId,
                        DateEnd = Input.DateEnd,
                        DateStart = Input.DateStart.Date,
                        DGIIDescription = Input.DGIIDescription,
                        CompanyId = Input.CompanyId,
                        SeqEnd = Input.SeqEnd,
                        SeqNext = Input.SeqNext,
                        SeqStart = Input.SeqStart,
                        SeqStatus = Input.SeqStatus,
                        Serie = Input.Serie,
                        MasterId = Input.MasterId,
                        Deleted = false

                    };

                    if (Input.Id == 0)
                    {
                        var userData = (User)new UserController().Get(_context, user.Id);

                        DateTime date = (Input.NcfId == 2 || Input.NcfId == 4) ?
                            new DateTime(new Controllers.GenericFunctions().GetTimeZone().Year + 50, 1, 1) :
                            new DateTime(new Controllers.GenericFunctions().GetTimeZone().Year + 50, 1, 1);
                        DateTime lastDate = date.AddSeconds(-1);

                        data.CompanyId = userData.CompanyId;
                        data.DateEnd = lastDate;
                        data.MasterId = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        data = (NcfSequenceDetail)_ncfSequenceDetail.Get(_context, Input.Id);
                        data.DateStart = Input.DateStart.Date;
                        data.DateEnd = Input.DateEnd.Date.AddDays(1).AddSeconds(-1);
                        data.SeqStart = Input.SeqStart;
                        data.SeqNext = Input.SeqNext;
                        data.SeqEnd = Input.SeqEnd;
                        data.DGIIDescription = Input.DGIIDescription;
                        data.Deleted = Input.Deleted;
                    }

                    _ncfSequenceDetail.Save(_context, data);
                }
                catch (Exception ex)
                {
                    Notify(Header, ex.Message, Models.Enum.NotificationType.error);
                    await OnGetAsync(Input.Id);
                    return Page();
                }

                Notify(Header);
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage("FiscalDetails");
            }
            
        }
    }
}
