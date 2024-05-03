using System.ComponentModel.DataAnnotations;

namespace JobFairManagementSystem.Models;

public class Notification
{
    public int Id { get; set; }


    [StringLength(255)]
    public string Message { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsRead { get; set; }

    [StringLength(36)]
    public string ReceiverId { get; set; }


    [StringLength(36)]
    public string? SenderId { get; set; }

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
        Message = message;
        SenderId = sid;
    }
}