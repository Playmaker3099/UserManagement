using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Model;

[ApiController]
[Route("api/v1/[controller]")]
public class RolesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public RolesController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
        return await _context.Roles.ToListAsync();
    }

    // NEW: Needed so CreateRole has a place to point to
    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetRole(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null) return NotFound();
        return role;
    }

    [HttpPost]
    public async Task<ActionResult<Role>> CreateRole(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        // This now points to the GetRole method above
        return CreatedAtAction(nameof(GetRole), new { id = role.RoleId }, role);
    }
}