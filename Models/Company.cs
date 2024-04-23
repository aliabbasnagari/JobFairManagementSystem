using System.ComponentModel;
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
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(13)]
        [RegularExpression(@"^\+?[0-9]{10,13}$")]
        public string Contact { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        public List<Recruiter>? Recruiters { get; set; }
        public bool IsVerified { get; set; }

    }
}
