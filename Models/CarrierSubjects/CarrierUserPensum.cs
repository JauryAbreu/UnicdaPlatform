
namespace UnicdaPlatform.Models.CareerSubjects
{
    public class CareerUserPensum
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string UserId { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public int CareerPensumId { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(10)]
        public string SessionCode { get; set; }
        public int PeriodCycle { get; set; }
        public int PeriodYear { get; set; }
        public string Note { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string UserIdTeacher { get; set; }
        public int Credit { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
