using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFairManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<AdministratorUser> AdministratorUsers { get; set; }
        public DbSet<RecruiterUser> RecruiterUsers { get; set; }
        public DbSet<CandidateUser> CandidateUsers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
