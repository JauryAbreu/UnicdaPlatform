
namespace UnicdaPlatform.Models.CarrierSubjects
{
    public class CarrierUserPensumDetails
    {
        public string UserId { get; set; }
        public string CareerPensumId { get; set; }
        public string SessionCode { get; set; }
        public int PeriodCycle { get; set; }
        public int PeriodYear { get; set; }
        public decimal FirstTest { get; set; }
        public decimal SecondTest { get; set; }
        public decimal Practice { get; set; }
        public decimal FinalTest { get; set; }
        public string UserIdTeacher { get; set; }
        public int Credit { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
