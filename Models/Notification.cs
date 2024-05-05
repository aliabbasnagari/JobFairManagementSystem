using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Models;

public class Notification
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Message { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsRead { get; set; }

    [Required]
    [StringLength(36)]
    [DisplayName("Receiver")]
    public string ReceiverId { get; set; }


    [StringLength(36)]
    public string? SenderId { get; set; }

    [StringLength(36)]
    [DisplayName("Receiver")]
    public string ApplicationUserId { get; set; }

    public Notification()
    {
        CreatedAt = DateTime.Now;
        IsRead = false;
    }

    public Notification(string sid, string rid, string message)
    {
        CreatedAt = DateTime.Now;
        IsRead = false;
        ReceiverId = rid;
        ApplicationUserId = rid;
        Message = message;
        SenderId = sid;
    }
}