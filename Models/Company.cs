using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Models
{
    public class Company
    {
        public string? Id { get; set; }

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

        [Required]
        [StringLength(500)]
        public string Description { get; set; }


        [StringLength(255)]
        public string? ContactEmail { get; set; }


        [StringLength(255)]
        public string? Venue { get; set; }

        public Schedule? InterviewSchedule { get; set; }
    }
}
