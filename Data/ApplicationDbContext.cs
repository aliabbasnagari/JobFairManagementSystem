using JobFairManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFairManagementSystem.Data;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    public DbSet<CompanyUser> Companies { get; set; }

    public DbSet<CandidateUser> Candidates { get; set; }

    public DbSet<AdminUser> Administrators { get; set; }

    public DbSet<InterviewSchedule> InterviewSchedules { get; set; }

    public DbSet<Slot> Slots { get; set; }



}