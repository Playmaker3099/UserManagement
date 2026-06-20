using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Model
{
    public class AuditLog
    {
        [Key]
        public int AuditId { get; set; }

        [Required]
        [StringLength(100)]
        public string AuditAction { get; set; } 

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; } 

        [Required]
        public DateTime AuditTimestamp { get; set; } = DateTime.UtcNow;

       
        //INFORMATION THAT WAS CHANGED

        [StringLength(255)]
        public string EntityName { get; set; } 

        public string Details { get; set; }

        [StringLength(50)]
        public string? IpAddress { get; set; } = "127.0.0.1";
    }
}