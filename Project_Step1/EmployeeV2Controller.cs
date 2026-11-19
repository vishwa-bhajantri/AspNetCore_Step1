using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;

namespace Project_Step1
{

    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeV2Controller : ControllerBase
    {

        public EmployeeV2Controller()
        {

        }

        public List<Employee> EmployeesData()
        {
            return new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Mail = "John.Doe@example.com", Location = "New York" },
                new Employee { Id = 2, Name = "Jane Smith", Mail = "Jane.Smith@example.com", Location = "Los Angeles" },
                new Employee { Id = 3, Name = "Mike Johnson", Mail = "Mike.Johnson@example.com", Location = "Chicago" }
            };
        }

        [HttpGet]
        [Route("GetEmployeesById")]
        public Employee GetEmployeeById(int id)
        {
            var employee = new Employee
            {
                Id = id,
                Name = $"Employee {id}",
                Mail = $"employee{id}@example.com"
            };
            return employee;
        }

        //By Querry Parameter

        [HttpGet]
        [Route("GetEmployeesByIdQuerry")]
        public Employee GetEmployeeByIdQuerry([FromQuery] int id)
        {
            var employee = new Employee
            {
                Id = id,
                Name = $"Employee {id}",
                Mail = $"employee{id}@example.com",
                Salary = 50000
            };
            return employee;
        }

        [HttpGet]
        [Route("GetEmployeeByIdHeader")]
        public dynamic GetEmployeeByIdHeader([FromHeader] string auth)
        {
            var anonymousUser = new
            {
                Id = 1,
                Name = $"Anonymous User",
                token = auth
            };
            return anonymousUser;
        }

        [HttpGet]
        [Route("GetEmployeeByBody")]
        public dynamic GetEmployeeByBody([FromBody] string auth)
        {
            var anonymousUser = new
            {
                Id = 1,
                Name = $"Anonymous User",
                token = auth
            };
            return anonymousUser;
        }
    }

}