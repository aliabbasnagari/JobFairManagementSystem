using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Data
{
    public class CompanyUser : ApplicationUser
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string ContactEmail { get; set; }
    }
}
