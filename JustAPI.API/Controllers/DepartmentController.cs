using JustAPI.Domain;
using JustAPI.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JustAPI.API.Controllers
{
    public class DepartmentController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public DepartmentController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetDepartments()
        {
            return await _dataContext.Departments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(Guid id)
        {
            if (id == default)
            {
                return BadRequest("ID is required");
            }
            var department = await _dataContext.Departments
                .SingleOrDefaultAsync(d => d.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            return department;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            if (department == null)
            {
                return BadRequest("Department is required");
            }
            _dataContext.Departments.Add(department);
            await _dataContext.SaveChangesAsync();
            return department;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> UpdateDepartment(Department department)
        {
            if (department == null)
            {
                return BadRequest("Department is required");
            }
            _dataContext.Departments.Update(department);
            await _dataContext.SaveChangesAsync();
            return department;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(Guid id)
        {
            if (id == default)
            {
                return BadRequest("ID is required");
            }
            var department = await _dataContext.Departments
                .SingleOrDefaultAsync(d => d.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            _dataContext.Departments.Remove(department);
            await _dataContext.SaveChangesAsync();
            return department;
        }
    }
}
