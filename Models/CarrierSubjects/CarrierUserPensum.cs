
namespace UnicdaPlatform.Models.CarrierSubjects
{
    public class CarrierUserPensum
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CareerPensumId { get; set; }
        public string SessionCode { get; set; }
        public int PeriodCycle { get; set; }
        public int PeriodYear { get; set; }
        public string Note { get; set; }
        public string UserIdTeacher { get; set; }
        public int Credit { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
