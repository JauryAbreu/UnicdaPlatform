using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnicdaPlatform.Controllers;
using UnicdaPlatform.Controllers.Companies;
using UnicdaPlatform.Controllers.Users;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Company;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Areas.Identity.Pages.Account.Manage.Administrator.Companies
{
    public class CompanyInformationModel : BasePage
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CompanyInformationModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context,
            IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public string Username { get; set; } public string Picture { get; set; } public string CompanyName { get; set; }
        private UserMainController _user = new UserMainController();
        private CompanyController _company = new CompanyController();
        private int companyId = 0;
        public Company company { get; set; }
        public string Header = "Empresa";
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : Company 
        {
            public int StateTemp { get; set; } = 0; 
        };

        private async Task LoadAsync(IdentityUser user)
        {
            company = (Company)_company.Get(_context, companyId);

            Input = new InputModel()
            {
                Id = company.Id,
                Currency = company.Currency,
                Address = company.Address,
                CompanyId = company.CompanyId,
                Country = company.Country,
                State = company.State,
                Email = company.Email,
                ReceiptId = company.ReceiptId,
                Phone = company.Phone,
                Deleted = company.Deleted,
                StateTemp = company.State,
                License = company.License,
                Logo = company.Logo,
                Name = company.Name,
                VatNumber = company.VatNumber,
                CompanyName = company.CompanyName,
                DGIITax = company.DGIITax,
                EmailSMTP = company.EmailSMTP,
                PasswordSMTP = company.PasswordSMTP
            };
        }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            #region Permission
            var permission = _user.GetUserDate(_context, user, PermissionAlias.Company);
            if (!permission.Item1)
            {
                Notify(Header, "No posee permisos para esta opción", Models.Enum.NotificationType.warning);
                return RedirectToPage("Index");
            }

            Username = permission.Item2;  
            Picture = permission.Item3; CompanyName = permission.Item5;
            id = (id == 0) ? permission.Item4 : id;
            #endregion

            companyId = id;
            await LoadAsync(user);
            return Page();
        }

       

        //public async Task<IActionResult> OnPostAsync()
        public async Task<IActionResult> OnPostUploadAsync()
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
                    if (string.IsNullOrEmpty(Input.Name))
                    {
                        Notify(Header, "Nombre no pueden estar vacio.", Models.Enum.NotificationType.warning);
                        await OnGetAsync(Input.Id);
                        return Page();
                    }
                    else if (string.IsNullOrEmpty(Input.Phone))
                    {
                        Notify(Header, "Telefono no pueden estar vacio.", Models.Enum.NotificationType.warning);
                        await OnGetAsync(Input.Id);
                        return Page();
                    }

                    var data = new Company();

                    if (Input.Id == 0)
                    {
                        data = new Company()
                        {
                            Id = Input.Id,
                            CompanyId = Input.CompanyId,
                            Name = Input.Name,
                            CompanyName = Input.CompanyName,
                            Currency = Input.Currency,
                            Address = Input.Address,
                            Country = Input.Country,
                            State = Input.State,
                            Email = Input.Email,
                            Phone = Input.Phone,
                            License = Input.License,
                            Logo = Input.Logo,
                            VatNumber = Input.VatNumber,
                            ReceiptId = 1,
                            DGIITax = Input.DGIITax,
                            Deleted = Input.Deleted,
                            EmailSMTP = Input.EmailSMTP,
                            PasswordSMTP = Input.PasswordSMTP
                        };
                        data.CompanyId = Guid.NewGuid().ToString();

                    }
                    else
                    {
                        data = (Company)_company.Get(_context, Input.Id);

                        data.Name = Input.Name;
                        data.Currency = Input.Currency;
                        data.Address = Input.Address;
                        data.Country = Input.Country;
                        data.State = Input.State;
                        data.CompanyName = Input.CompanyName;
                        data.Email = Input.Email;
                        data.Phone = Input.Phone;
                        data.License = Input.License;
                        data.Logo = Input.Logo;
                        data.VatNumber = Input.VatNumber;
                        data.DGIITax = Input.DGIITax;
                        data.Deleted = Input.Deleted;
                        data.EmailSMTP = Input.EmailSMTP;
                        data.PasswordSMTP = Input.PasswordSMTP;
                    }


                    if (Input.LogoFile != null)
                        data.Logo = new GenericFunctions().UploadImage(_hostEnvironment, data.CompanyId, "Companies", Input.LogoFile);

                    _company.Save(_context, data);
                }
                catch (Exception ex)
                {
                    Notify(Header, ex.Message, Models.Enum.NotificationType.error);
                    await OnGetAsync(Input.Id);
                    return Page();
                }

                Notify(Header);
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage("CompanyDetails");
            }
            
        }
    }
}
