using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Models;

public class CreateScheduleVM
{
    public Schedule? Schedule { get; set; }
    
    [Required]
    public Slot Slot { get; set; }

}