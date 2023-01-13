using Microsoft.AspNetCore.Mvc;
using yungching_quiz.Context;
using yungching_quiz.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public Employee Get(string id)
        {
            return _pubsContext.Employees.Where(row => row.EmpId == id).First();
        }

        // POST api/<Employee>
        [HttpPost]
        public void Post(Employee employee)
        {
            _pubsContext.Add(employee);
            _pubsContext.SaveChanges();
        }

        // DELETE api/<Employee>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var employee = _pubsContext.Employees.Where(row => row.EmpId == id);
            _pubsContext.Remove(employee);
        }
    }
}
