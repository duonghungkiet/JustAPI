using JustAPI.Domain;
using JustAPI.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JustAPI.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public UsersController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _dataContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            if (id == default)
            {
                return BadRequest("ID is required");
            }

            var user = await _dataContext.Users
                .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User is required");
            }
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User is required");
            }
            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            if (id == default)
            {
                return BadRequest("ID is required");
            }
            var user = await _dataContext.Users
                .SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }
}