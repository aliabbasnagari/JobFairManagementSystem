namespace JobFairManagementSystem.Models;

public class InterviewSlot
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public bool IsFree { get; set; }

}
