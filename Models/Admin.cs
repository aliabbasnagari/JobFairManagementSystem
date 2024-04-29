using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Models;

public class Admin
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
    public string Password { get; set; }

    [Required]
    [StringLength(255)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    [StringLength(15)]
    [RegularExpression(@"^\d{5}-\d{7}-\d", ErrorMessage = "CNIC format is xxxxx-xxxxxxx-x")]
    public string CNIC { get; set; }

    [Required]
    [StringLength(15)]
    [RegularExpression(@"^\+(?:\d{1,3}\s?){1,3}\d{6,14}$", ErrorMessage = "Phone format is +xxxxxxxxxxxx")]
    public string PhoneNumber { get; set; }
}