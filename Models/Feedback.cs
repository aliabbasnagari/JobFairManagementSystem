using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobFairManagementSystem.Data;

namespace JobFairManagementSystem.Models;

public class Feedback
{
    public int Id { get; set; }

    [Required]
    [StringLength(500)]
    [DisplayName("Feedback")]
    public string Message { get; set; }

    public virtual ApplicationUser? FromUser { get; set; }

    [ForeignKey("FromUser")]
    public string? FromUserId { get; set; }

    public virtual ApplicationUser ToUser { get; set; }

    [ForeignKey("ToUser")]
    [Required]
    [DisplayName("Feedback For")]

    public string ToUserId { get; set; }

    [NotMapped]
    public bool Anonymous { get; set; }

}