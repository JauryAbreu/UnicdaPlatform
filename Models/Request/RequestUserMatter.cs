namespace UnicdaPlatform.Models.Request
{
    public class RequestUserMatter
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CareerPensumId { get; set; }
        public string SessionCode { get; set; }
        public int PeriodCycle { get; set; }
        public int PeriodYear { get; set; }
        public string Comment { get; set; }
        public string UserResponseId { get; set; }
        public string ResponseComment { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
