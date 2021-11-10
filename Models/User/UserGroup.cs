namespace UnicdaPlatform.Models.User
{
    public class UserGroup
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(36)]
        public string GroupId { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(36)]
        public string CompanyId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
