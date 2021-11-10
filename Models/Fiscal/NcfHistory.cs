using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnicdaPlatform.Models.Fiscal
{
    public class NcfHistory
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(36)]
        public string CompanyId { get; set; }
        [Required]
        [MaxLength(20)]
        public string ReceiptId { get; set; }
        public int NcfType { get; set; }
        [Required]
        [MaxLength(20)]
        public string NcfNumber { get; set; }
        [Required]
        [MaxLength(20)]
        public string ReturnReceiptId { get; set; }
        [Required]
        [MaxLength(20)]
        public string ReturnNcfNumber { get; set; }
        [Required]
        [MaxLength(30)]
        public string VatNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string Company { get; set; }
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal TotalAmount { get; set; }
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal TotalAmountWithTax { get; set; }
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal TotalTax { get; set; }
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal CurrencyChange { get; set; }
        public bool TaxExempt { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
