using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Models;

public class CreateScheduleVM
{
    public InterviewSchedule? InterviewSchedule { get; set; }
    
    [Required]
    public Slot? Slot { get; set; }

}