using System;

namespace UnicdaPlatform.Models.Trans
{
    public class Batch
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string BatchId { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithTax { get; set; }
        public decimal TotalReturnAmount { get; set; }
        public decimal TotalReturnAmountWithTax { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
