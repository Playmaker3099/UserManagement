using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Model
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        // The Foreign Key (This links to the Role table)
        [Required]
        public int RoleId { get; set; }

        // Navigation Property 
        // This allows you to access user.Role.RoleName without 
        // storing the name twice in the database.
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
