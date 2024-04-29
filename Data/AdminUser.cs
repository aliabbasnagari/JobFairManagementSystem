using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Data
{
    public class AdminUser : ApplicationUser
    {
        [Required]
        [StringLength(15)]
        public string CNIC { get; set; }
    }
}
