using System.ComponentModel;
using JobFairManagementSystem.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Models;

public class Slot
{
    public int Id { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }
    public bool Reserved { get; set; }
    public bool InterviewConducted { get; set; }

    public virtual CandidateUser? Candidate { get; set; }

    public string? CandidateId { get; set; }


    public bool IsOverlapping(Slot otherSlot)
    {
        return (StartTime < otherSlot.EndTime && EndTime > otherSlot.StartTime);
    }

    public override string ToString()
    {
        return StartTime.ToString("HH:mm:ss") + " - " + EndTime.ToString("HH:mm:ss");
    }
}