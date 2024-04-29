using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Data
{
    public class CandidateUser : ApplicationUser
    {
        [Required]
        [StringLength(255)]
        public string Address { get; set; }


        [Required]
        [StringLength(15)]
        public string CNIC { get; set; }

        [Required]
        [StringLength(6)]
        public string Gender { get; set; }  

        public DateTime DateOfBirth { get; set; }

        public string? Degree { get; set; }

        public DateTime GraduationDate { get; set; }

        public float CGPA { get; set; }

        public List<string>? Skills { get; set; }

        public List<string>? SocialLinks { get; set; }

        [StringLength(500)]
        public string? Bio { get; set; }
    }
}
