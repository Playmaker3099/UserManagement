using System.ComponentModel.DataAnnotations;

namespace UserManagement.Model
{
    public enum TaskStatus
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3,
        OnHold = 4
    }
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }

        public int AssignedToUserId { get; set; }
        public Users AssignedToUser { get; set; }

        public TaskStatus TaskStatus { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; } // The '?' makes it optional
    }
}
