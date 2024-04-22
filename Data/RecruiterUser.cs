using Microsoft.AspNetCore.Identity;

namespace JobFairManagementSystem.Data;

public class RecruiterUser : IdentityUser
{
    public string AddressTesterA { get; set; }
}