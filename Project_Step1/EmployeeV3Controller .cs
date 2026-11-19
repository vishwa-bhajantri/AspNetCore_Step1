using Microsoft.AspNetCore.Mvc;

namespace Step1.ControllerHTTPVerbs
{

    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeV3Controller : ControllerBase
    {

        public EmployeeV3Controller()
        {

        }

        private static List<Employee> employees = new List<Employee>

            {
                new Employee { Id = 1, Name = "John Doe", Mail = "John.Doe@example.com"},
                new Employee { Id = 2, Name = "Jane Smith", Mail = "Jane.Smith@example.com"},
                new Employee { Id = 3, Name = "Mike Johnson", Mail = "Mike.Johnson@example.com" },
                new Employee { Id = 4, Name = "Kane Smith", Mail = "Kane.Smith@example.com" }
            };

        [HttpGet("GetAllEmployees")]
        public List<Employee> GetAllEmployees()
        {
            return employees;
        }

        [HttpGet("GetEmployeeById/{id}")]
        public Employee GetEmployeeById([FromRoute] int id)
        {
            // return employees.First(x => x.Id == id);         //This will throw an exception if the employee with the specified id is not found
            return employees.FirstOrDefault(x => x.Id == id);   //This will return null if the employee with the specified id is not found
        }

        [HttpGet("SearchEmployees")]
        public List<Employee> SearchEmployees([FromQuery] string name)
        {
            return employees.Where(x => x.Name.Contains(name)).ToList();

        }

        [HttpGet("GetEmployeeByIdHeader")]
        public Employee GetEmployeeByIdHeader([FromHeader (Name ="EmployeeId")] int id)
        {
            return employees.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost("CreateEmployee")]
        public Employee CreateEmployee([FromBody] Employee employee)
        {
            employee.Id = employees.Count == 0 ? 1 : employees.Max(x => x.Id) + 1;

            //if(employee.Id == 0)
            //{
            //    employee.Id = 1;
            //}
            //else
            //{
            //    employee.Id = employees.Max(x => x.Id) + 1;
            //}

            employees.Add(employee);
            return employee;
        }

        [HttpPut("UpdateEmployee/{id}")]
        public Employee? UpdateEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            Employee? existingEmployee = employees.FirstOrDefault(x => x.Id == id);
            if (existingEmployee == null)
            {
                return null;
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Mail = employee.Mail;
            return existingEmployee;
        }

        [HttpPatch("PatchEmployee/{id}")]
        public Employee? PatchEmployee([FromRoute] int id, [FromBody] EmployeePatch employeePatch)
        {
            Employee? existingEmployee = employees.FirstOrDefault(x => x.Id == id);
            if (existingEmployee == null)
            {
                return null;
            }
            if (employeePatch.Name != null)
            {
                existingEmployee.Name = employeePatch.Name;
            }
            if (employeePatch.Mail != null)
            {
                existingEmployee.Mail = employeePatch.Mail;
            }
            return existingEmployee;
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public bool DeleteEmployee([FromRoute] int id)
        {
            Employee? existingEmployee = employees.FirstOrDefault(x => x.Id == id);
            if (existingEmployee == null)
            {
                return false;
            }
            employees.Remove(existingEmployee);
            return true;
        }
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Mail { get; set; }

        }

        public class EmployeePatch
        {
            public string Name { get; set; }
            public string Mail { get; set; }
        }
    }
}