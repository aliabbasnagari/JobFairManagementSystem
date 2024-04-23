namespace JobFairManagementSystem.Models
{
    public class InterviewSchedule
    {
        public int Id { get; set; }
        List<InterviewSlot> InterviewSlots { get; set; }
    }
}
