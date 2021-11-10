using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnicdaPlatform.Models.Fiscal
{
    public class NcfSequenceDetail
    {
        public int Id { get; set; }
        [MaxLength(36)]
        public string MasterId { get; set; }
        [MaxLength(36)]
        public string CompanyId { get; set; }
        public int NcfId { get; set; }
        [Required]
        [MaxLength(3)]
        public string Serie { get; set; }
        public int SeqStart { get; set; }
        public int SeqEnd { get; set; }
        public int SeqNext { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int SeqStatus { get; set; }
        [Required]
        [MaxLength(50)]
        public string DGIIDescription { get; set; }
        public bool Deleted { get; set; }
    }
}
