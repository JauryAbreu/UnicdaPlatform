using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicdaPlatform.Models.Company
{
    public class Company
    {
        public int Id { get; set; }
        [MaxLength(36)]
        public string CompanyId { get; set; } = "";
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(30)]
        public string VatNumber { get; set; } = "";
        [MaxLength(100)]
        public string CompanyName { get; set; }
        public int State { get; set; } = 0;
        public int Country { get; set; } = 0;
        [MaxLength(300)]
        public string Address { get; set; } = "";
        [MaxLength(100)]
        public string Email { get; set; } = "";
        [MaxLength(30)]
        public string Phone { get; set; } = "";
        public string Logo { get; set; } = "";

        [NotMapped]
        public IFormFile LogoFile { get; set; }
        public decimal Currency { get; set; } = 0;
        public string License { get; set; } = "";
        public decimal DGIITax { get; set; } = 1;
        public int ReceiptId { get; set; } = 1;
       
        public string EmailSMTP { get; set; } = "";
        public string PasswordSMTP { get; set; } = "";
        public bool Deleted { get; set; } = false;

        
    }
}
