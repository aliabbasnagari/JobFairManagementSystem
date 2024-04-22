using Microsoft.AspNetCore.Identity;

namespace JobFairManagementSystem.Data
{
    public class AdministratorUser: IdentityUser
    {
        public string AddressTesterB { get; set; }
    }
}
