using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace JobFairManagementSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [StringLength(13)]
        [RegularExpression(@"^\d{13}$")]
        public string Cnic { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(6)]
        public string Gender { get; set; }
    }
}
