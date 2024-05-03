using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobFairManagementSystem.Models;

namespace JobFairManagementSystem.Data;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    [StringLength(255)]
    public string Password { get; set; }

    [Required]
    [StringLength(255)]
    [Compare("Password")]
    [NotMapped]
    public string ConfirmPassword { get; set; }

    public bool IsVerified { get; set; }

    public List<Notification> Notifications { get; set; }

    public ApplicationUser()
    {
        UserName = Id;
    }

}