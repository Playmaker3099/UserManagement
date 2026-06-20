using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Model;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] // This makes the URL: api/users
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            // We use .Include(u => u.Role) so the response shows the role details
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<Users>> PostUser(Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // AUTOMATIC AUDIT LOG
            var log = new AuditLog
            {
                AuditAction = "User Created",
                UserId = user.UserId, // Tracking the new user
                EntityName = "Users",
                Details = $"User {user.UserName} was created with Role ID {user.RoleId}",
                AuditTimestamp = DateTime.UtcNow
            };
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // PUT: api/users/5 (Update)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Users user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // AUDIT LOG for Update
                _context.Logs.Add(new AuditLog
                {
                    AuditAction = "User Updated",
                    UserId = id,
                    EntityName = "Users",
                    Details = $"Updated info for {user.UserName}"
                });
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);

            // AUDIT LOG for Deletion
            _context.Logs.Add(new AuditLog
            {
                AuditAction = "User Deleted",
                UserId = id,
                EntityName = "Users",
                Details = $"Deleted user: {user.UserName}"
            });

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}