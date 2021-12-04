using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicdaPlatform.Models.CareerSubjects
{
    public class ApiMatterData
    {

        [System.ComponentModel.DataAnnotations.Key]
        public string StudentId { get; set; }
        public string MatterId { get; set; }
        public int SessionCode { get; set; }
        public int PeriodCycle { get; set; }
        public int PeriodYear { get; set; }
        public string UserResponseId { get; set; }
        public string UserResponseMsg { get; set; }
        public bool IsApproved { get; set; }
    }
}