namespace UnicdaPlatform.Models.CareerSubjects
{
    public class UserMatter
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string CompanyId { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string UserId { get; set; }
        public int CareerPensumId { get; set; }
        public bool Deleted { get; set; }
    }


}
