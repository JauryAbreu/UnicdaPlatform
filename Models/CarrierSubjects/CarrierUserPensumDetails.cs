
namespace UnicdaPlatform.Models.CareerSubjects
{
    public class CareerUserPensumDetails
    {
        public int Id { get; set; }
        public int CareerUserPensumId { get; set; }
        public decimal FirstTest { get; set; }
        public decimal SecondTest { get; set; }
        public decimal Practice { get; set; }
        public decimal FinalTest { get; set; }
        public decimal Total { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
