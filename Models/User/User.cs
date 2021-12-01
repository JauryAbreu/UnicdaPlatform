namespace UnicdaPlatform.Models.User
{
    public class User
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(36)]
        public string MasterId { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(36)]
        public string CompanyId { get; set; }
        public int GroupId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(20)]
        public string UserId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string FirstName { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string LastName { get; set; }
        //[System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(300)]
        public string Address { get; set; }
        public int Gender { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(36)]
        public string CareerId { get; set; }
        public string VatNumber { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(30)]
        public string Phone { get; set; }
        public decimal Average { get; set; }
        public bool Deleted { get; set; }
    }
}
