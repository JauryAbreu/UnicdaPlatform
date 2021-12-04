
using System;

namespace UnicdaPlatform.Models.CareerSubjects
{
    public class CareerUserTeacherPensum
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string UserId { get; set; }
        public int CareerPensumId { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(10)]
        public string SessionCode { get; set; }
        public int PeriodCycle { get; set; }
        public int PeriodYear { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(20)]
        public string ClassRoom { get; set; }
        public int Day { get; set; }
        public DateTime TimeToIn { get; set; }
        public DateTime TimeToOut { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
