using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Model;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/taskitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            return await _context.Tasks
                .Include(t => t.AssignedToUser)
                .ToListAsync();
        }

        // POST: api/taskitems
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem task)
        {
            task.CreatedAt = DateTime.UtcNow;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Audit the task creation
            _context.Logs.Add(new AuditLog
            {
                AuditAction = "Task Created",
                UserId = task.AssignedToUserId,
                EntityName = "Tasks",
                Details = $"Task assigned to User {task.AssignedToUserId}: {task.TaskDescription}"
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasks), new { id = task.TaskId }, task);
        }

        // PUT: api/taskitems/5 (Update Status or Description)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem task)
        {
            if (id != task.TaskId) return BadRequest();

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Audit the change (e.g., status changed to 'Completed')
                _context.Logs.Add(new AuditLog
                {
                    AuditAction = "Task Updated",
                    UserId = task.AssignedToUserId,
                    EntityName = "Tasks",
                    Details = $"Task {id} status updated to {task.TaskStatus}"
                });
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(e => e.TaskId == id)) return NotFound();
                throw;
            }

            return NoContent();
        }
    }
}