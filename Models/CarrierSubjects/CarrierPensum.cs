namespace UnicdaPlatform.Models.CarrierSubjects
{
    public class CarrierPensum
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string CareerId { get; set; }
        public string CareerPensumId { get; set; }
        public string Description { get; set; }
        public string PreCareerPensumId { get; set; }
        public int Credit { get; set; }
        public bool Deleted { get; set; }
    }
}
