using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFairManagementSystem.Data;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    public DbSet<CompanyUser> Companies { get; set; }

    public DbSet<CandidateUser> Students { get; set; }

    public DbSet<AdminUser> Administrators { get; set; }

}