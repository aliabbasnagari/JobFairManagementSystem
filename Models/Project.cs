using System.ComponentModel.DataAnnotations;
using JobFairManagementSystem.Data;

namespace JobFairManagementSystem.Models;

public class Project
{
    public int Id { get; set; }

    [StringLength(255)]
    public string Title { get; set; }


    [StringLength(1000)]
    public string Description { get; set; }
}