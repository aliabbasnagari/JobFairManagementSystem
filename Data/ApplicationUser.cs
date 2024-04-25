using Microsoft.AspNetCore.Identity;

namespace JobFairManagementSystem.Data;

public class ApplicationUser : IdentityUser
{
    public bool IsVerified { get; set; }
}