using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Model;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuditLogsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuditLogsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditLog>>> GetLogs()
        {
            return await _context.Logs
                .Include(l => l.User)
                .OrderByDescending(l => l.AuditTimestamp)
                .ToListAsync();
        }
    }
}
