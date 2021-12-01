namespace UnicdaPlatform.Models.CareerSubjects
{
    public class Matter
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string CompanyId { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(20)]
        public string MatterId { get; set; }
        public string Description { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(20)]
        public string PreMatterId { get; set; }
        public int Credit { get; set; }
        public bool Deleted { get; set; }
    }
}
