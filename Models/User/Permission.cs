namespace UnicdaPlatform.Models.User
{
    public class Permission
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(36)]
        public string PermissionId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
