using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnicdaPlatform.Controllers.Fiscal;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Fiscal;
using UnicdaPlatform.Models.Fiscal.Enum;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Fiscals
{
    public class FiscalDetailsModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public FiscalDetailsModel(
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
        private NcfTypeController _ncfType = new NcfTypeController();
        private NcfSequenceDetailController _ncfSequenceDetail = new NcfSequenceDetailController();
        public List<NcfType> ncfTypes { get; set; }
        public List<NcfSequenceDetail> ncfSequenceDetails { get; set; }
        public int ncfTypeId { get; set; }
        public string NcfName { get; set; }
        public string Header = "Información Fiscal";
        private async Task LoadAsync(IdentityUser user)
        {
            ncfTypes = _ncfType.GetList(_context);
            ncfSequenceDetails = _ncfSequenceDetail.GetList(_context, ncfTypeId);
        }

        public async Task<IActionResult> OnGetAsync(int Id = 2)
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
            this.ncfTypeId = Id;
            GetNcfName();
            #endregion
            await LoadAsync(user);
            return Page();
        }

        private void GetNcfName()
        {
            switch (ncfTypeId)
            {
                case 1:
                    NcfName = "Credito Fiscal";

                    break;
                case 2:
                    NcfName = "Consumidor Fiscal";
                    break;
                case 3:
                    NcfName = "Nota de Debito";
                    break;
                case 4:
                    NcfName = "Nota de Credito";
                    break;
                case 14:
                    NcfName = "Regimen Especial";
                    break;
                case 15:
                    NcfName = "Gubernamental";
                    break;
            }
        }

        //[HttpPost]
        public async Task<IActionResult> OnPostAsync(int fiscalId, int AddSequenceId, int view, int delete)
        {
            if (fiscalId > 0) 
            {
                return RedirectToPage("FiscalDetails", new { id = fiscalId });
            }
            if (AddSequenceId > 0)
            {
                return RedirectToPage("FiscalInformation", new { ncfType = AddSequenceId });
            }
            else if (view > 0)
            {
                return RedirectToPage("FiscalInformation", new { id = view });
            }
            else if (delete > 0)
            {
                if (_context.NcfSequenceDetail.Count(a => a.Id == delete) == 1)
                {
                    var line = (NcfSequenceDetail)_ncfSequenceDetail.Get(_context, delete);

                    if (line.SeqStatus == (int)NCFSequenceStatusEnum.Secuencia_Pendiente)
                    {
                        _ncfSequenceDetail.Delete(_context, delete);
                        Notify(Header, "Sencuencia Eliminada");
                    }
                    else 
                        Notify(Header, "Sencuencia esta en uso o fue utilizada. No se puede eliminar", Models.Enum.NotificationType.warning);
                }
            }
            return RedirectToPage();
        }
    }
}
