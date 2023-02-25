using EmployeeManagementSystem.DAL.Data.Interface;
using EmployeeManagementSystem.DTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository) => _employeeRepository = employeeRepository;

        [HttpGet]
        public async Task<List<EmployeeDto>> Get() => await _employeeRepository.List();

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            EmployeeDto employee = await _employeeRepository.Read(id);

            return employee is not null ? Ok(employee) : NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Post(EmployeeDto employee) => await _employeeRepository.Create(employee) ? StatusCode(StatusCodes.Status201Created) : BadRequest();

        [HttpPut("Update")]
        public async Task<ActionResult> Update(EmployeeDto employee) => await _employeeRepository.Update(employee) ? Ok(employee) : BadRequest();

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) => await _employeeRepository.Delete(id) ? Ok() : BadRequest();
    }
}
