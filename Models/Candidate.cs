using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Models;

public class Candidate
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [StringLength(255)]
    public string Address { get; set; }

    [Required]
    [StringLength(255)]
    public string Password { get; set; }

    [Required]
    [StringLength(255)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    [StringLength(15)]
    [RegularExpression(@"^\d{5}-\d{7}-\d")]
    public string CNIC { get; set; }

    [Required]
    [StringLength(6)]
    public string Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Degree { get; set; }

    public DateTime GraduationDate { get; set; }

    public float CGPA { get; set; }

    public List<string>? Skills { get; set; }

    public List<string>? SocialLinks { get; set; }

    [StringLength(500)]
    public string? Bio { get; set; }

    public Schedule? ProjectSchedule { get; set; }
}