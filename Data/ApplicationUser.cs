using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Data;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    public bool IsVerified { get; set; }

    public ApplicationUser()
    {
        UserName = Id;
    }

}