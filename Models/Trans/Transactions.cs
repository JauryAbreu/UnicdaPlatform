using System;

namespace UnicdaPlatform.Models.Trans
{
    public class Transactions
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string ReceiptId { get; set; }
        public string UserId { get; set; }
        public bool IsReturn { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithTax { get; set; }
        public decimal TotalTax { get; set; }
        public bool TaxExempt { get; set; }
        public string BatchClose { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
