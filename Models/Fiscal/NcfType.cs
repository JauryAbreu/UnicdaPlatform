using System.ComponentModel.DataAnnotations;

namespace UnicdaPlatform.Models.Fiscal
{
    public class NcfType
    {
        public int Id { get; set; }
        public int NcfId { get; set; }
        public bool IsDefaultSale { get; set; }
        public bool IsDefaultCreditMemo { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public bool Deleted { get; set; }
    }
}
