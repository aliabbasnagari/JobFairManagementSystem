namespace JobFairManagementSystem.Models;

public class InterviewSchedule
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public virtual List<Slot>? Slots { get; set; }

}