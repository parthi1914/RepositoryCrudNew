using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositoryCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _iEmployeeRepository;
        public EmployeeController(IEmployeeRepository iemployeeRepository)
        {
            _iEmployeeRepository = iemployeeRepository;
        }


        // GET: api/TrainingStaffs/5
        [HttpGet("GetEmployees")]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var employees = await _iEmployeeRepository.GetEmployees(); 
            return employees;
        }


        
        [HttpGet("GetEmployeeByID")]
        public async Task<ActionResult<Employee>> GetEmployeeByID(int id)
        {
            var employee = await _iEmployeeRepository.GetEmployee(id);
            return employee;
        }


        [HttpPost("SaveEmployee")]
        public async Task<ActionResult<Employee>> SaveEmployee(Employee employee)
        {
            Employee employeeResponse = new Employee(); 
            if (employee != null)
            {
                employee.EmployeeId = 0;
                employeeResponse = await _iEmployeeRepository.AddEmployee(employee);
            }
            
            return employeeResponse;
        }



        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
