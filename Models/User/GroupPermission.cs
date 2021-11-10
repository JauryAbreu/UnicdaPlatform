
namespace UnicdaPlatform.Models.User
{
    public class GroupPermission
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(36)]
        public string GroupId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(36)]
        public string PermissionId { get; set; }
        public bool Deleted { get; set; }
    }
}
