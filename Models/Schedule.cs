namespace JobFairManagementSystem.Models;

public class Schedule
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public virtual List<Slot> Slots { get; set; }
}