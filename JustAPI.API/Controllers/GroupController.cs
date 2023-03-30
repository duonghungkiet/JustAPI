using JustAPI.Domain;
using JustAPI.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JustAPI.API.Controllers
{
    public class GroupController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public GroupController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Group>>> GetGroups()
        {
            return await _dataContext.Groups.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(Guid id)
        {
            if (id == default)
            {
                return BadRequest("ID is required");
            }
            var group = await _dataContext.Groups
                .SingleOrDefaultAsync(g => g.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            return group;
        }

        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroup(Group group)
        {
            if (group == null)
            {
                return BadRequest("Group is required");
            }
            _dataContext.Groups.Add(group);
            await _dataContext.SaveChangesAsync();
            return group;
        }

        [HttpPost]
        public async Task<ActionResult<Group>> UpdateGroup(Group group)
        {
            if (group == null)
            {
                return BadRequest("Group is required");
            }
            _dataContext.Groups.Update(group);
            await _dataContext.SaveChangesAsync();
            return group;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Group>> DeleteGroup(Guid id)
        {
            if (id == default)
            {
                return BadRequest("ID is required");
            }
            var group = await _dataContext.Groups
                .SingleOrDefaultAsync(g => g.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            _dataContext.Groups.Remove(group);
            await _dataContext.SaveChangesAsync();
            return group;
        }
    }
}
