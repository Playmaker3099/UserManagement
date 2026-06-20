using System.ComponentModel.DataAnnotations;

namespace UserManagement.Model
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } // e.g., "Admin", "Technician", "Manager"
    }
}
