
using System;

namespace UnicdaPlatform.Models.CarrierSubjects
{
    public class CarrierUserTeacherPensum
    {
        public string UserId { get; set; }
        public string CareerPensumId { get; set; }
        public string SessionCode { get; set; }
        public int PeriodCycle { get; set; }
        public int PeriodYear { get; set; }
        public string ClassRoom { get; set; }
        public int Day { get; set; }
        public DateTime TimeToIn { get; set; }
        public DateTime TimeToOut { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
