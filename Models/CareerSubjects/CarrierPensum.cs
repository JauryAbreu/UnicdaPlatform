namespace UnicdaPlatform.Models.CareerSubjects
{
    public class CareerPensum
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string CareerId { get; set; }
        public string MatterId { get; set; }
        public int Period { get; set; }
        public bool Deleted { get; set; }
    }
}
