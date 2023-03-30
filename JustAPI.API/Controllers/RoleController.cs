using JustAPI.Domain;
using JustAPI.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JustAPI.API.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public RoleController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetRoles()
        {
            return await _dataContext.Roles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(Guid id)
        {
            if (id == default)
            {
                return BadRequest("ID is required");
            }
            var role = await _dataContext.Roles
                .SingleOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return role;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(Role role)
        {
            if (role == null)
            {
                return BadRequest("Role is required");
            }
            _dataContext.Roles.Add(role);
            await _dataContext.SaveChangesAsync();
            return role;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> UpdateRole(Role role)
        {
            if (role == null)
            {
                return BadRequest("Role is required");
            }
            _dataContext.Roles.Update(role);
            await _dataContext.SaveChangesAsync();
            return role;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(Guid id)
        {
            if (id == default)
            {
                return BadRequest("ID is required");
            }
            var role = await _dataContext.Roles
                .SingleOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            _dataContext.Roles.Remove(role);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
