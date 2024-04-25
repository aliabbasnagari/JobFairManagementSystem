using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Models;

public class LoginVM
{
    [Required]
    [StringLength(255)]
    public string? Email { get; set; }

    [Required]
    [StringLength(255)]
    public string? Password { get; set; } 

    [Required]
    public bool RememberMe { get; set; }
}