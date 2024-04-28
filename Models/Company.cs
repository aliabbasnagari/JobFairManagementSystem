using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
