using System;

namespace UnicdaPlatform.Models.Trans
{
    public class TransactionsDetails
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string ReceiptId { get; set; }
        public bool IsReturn { get; set; }
        public string SessionCode { get; set; }
        public int PeriodCycle { get; set; }
        public int PeriodYear { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithTax { get; set; }
        public decimal TotalTax { get; set; }
        public bool TaxExempt { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
