using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using yungching_quiz.Context;
using yungching_quiz.Data;

namespace yungching_quiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PubsContext _pubsContext;

        public EmployeeController(PubsContext context)
        {
            _pubsContext = context;
        }

        [HttpGet("{action}")]
        public List<Employee> GetAll()
        {
            return _pubsContext.Employees.ToList();
        }

        // GET api/<Employee>/5
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(string id)
        {
            var employee = _pubsContext.Employees.Where(row => row.EmpId == id).FirstOrDefault();
            if (employee == null) return NotFound();
            return employee;
        }

        // POST api/<Employee>
        [HttpPost]
        public async Task<ActionResult<Employee>> Insert(Employee employee)
        {
            try
            {
                var emp = _pubsContext.Employees.AddAsync(employee);
                await _pubsContext.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = employee.EmpId }, employee);
            }
            catch(Exception dnex)
            {
                return CreatedAtAction(nameof(Get), new { ex = dnex.ToString() });
            }
        }

        // DELETE api/<Employee>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            Employee? employee = _pubsContext.Employees.Where(row => row.EmpId == id).FirstOrDefault();
            if (employee == null) return NotFound();

            _pubsContext.Remove(employee);
            await _pubsContext.SaveChangesAsync();
            return Ok();
        }
    }
}
