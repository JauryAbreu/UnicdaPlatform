namespace UnicdaPlatform.Models.Request
{
    public class Complaints
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public string UserResponseId { get; set; }
        public string ResponseComment { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
