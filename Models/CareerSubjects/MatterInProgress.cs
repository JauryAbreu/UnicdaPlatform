namespace UnicdaPlatform.Models.CareerSubjects
{
    public class MatterInProgress
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string MatterId { get; set; }
        public string Description { get; set; }
        public int Credit { get; set; }
        public decimal FirstTest { get; set; }
        public decimal SecondTest { get; set; }
        public decimal Practice { get; set; }
        public decimal FinalTest { get; set; }
        public decimal Total { get; set; }
        public string Teacher_Name { get; set; }
        public int CareerPensumId { get; set; }
    }
}
