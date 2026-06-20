using Microsoft.EntityFrameworkCore;
using UserManagement.Model;

namespace UserManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<AuditLog> Logs { get; set; }
        public DbSet<Role> Roles { get; set; }


        // Inside your ApplicationDbContext.cs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .Property(t => t.TaskStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "Admin" },
            new Role { RoleId = 2, RoleName = "Manager" },
            new Role { RoleId = 3, RoleName = "Staff" });
        }
    }
    }
