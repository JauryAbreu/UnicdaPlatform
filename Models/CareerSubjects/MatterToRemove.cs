
namespace UnicdaPlatform.Models.CareerSubjects
{
    public class MatterToRemove
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string MatterId { get; set; }
        public string Description { get; set; }
        public int SessionCode { get; set; }
        public int PeriodCycle { get; set; }
        public int PeriodYear { get; set; }
    }
}
