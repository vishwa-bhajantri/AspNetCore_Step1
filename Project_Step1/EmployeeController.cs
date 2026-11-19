using Microsoft.AspNetCore.Mvc;

namespace Project_Step1
{

    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeController : ControllerBase
    {

        public EmployeeController()
        {

        }
        
        [HttpGet]
        [Route("EmployeeTestData")]
        public string GetEmployeeName()
        {
            return "Test Data - John";
        }

        [HttpGet]
        [Route("EmployeeFirstRecord/{customerName}")]
        public string GetEmployeeFirstName(string customerName)
        {
            return $"Employee Name is - {customerName}";
        }

        [HttpGet]
        [Route("GetEmployeesList")]
        public List<Employee> GetEmployeesList()
        {
            return new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Mail = "John.Doe@example.com", Location = "New York" },
                new Employee { Id = 2, Name = "Jane Smith", Mail = "Jane.Smith@example.com", Location = "Los Angeles" },
                new Employee { Id = 3, Name = "Mike Johnson", Mail = "Mike.Johnson@example.com", Location = "Chicago" }
            };
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

        //route - parameter

        [HttpGet]
        [Route("GetEmployeesByName/{name}")]
        public List<Employee> GetEmployeesByName(string name)
        {
            var employees = EmployeesData();
            return employees.Where(x => x.Name == name).ToList();
        }
    }

 }