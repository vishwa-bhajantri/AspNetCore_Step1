using Microsoft.AspNetCore.Mvc;

namespace Step1.StatusCodes
{

    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeV4Controller : ControllerBase
    {

        public EmployeeV4Controller()
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
        public IActionResult GetAllEmployees()
        {
            return Ok(employees);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public IActionResult GetEmployeeById([FromRoute] int id)
        {
            if(id == 1 || id == 2)
            {
                return Ok("Employee found.");    // 200 OK
            }
            return NotFound($"Employee with ID {id} not found.");     // 404 Not Found
        }

        //[HttpGet("GetEmployeeById/{id}")]
        //public Employee GetEmployeeById([FromRoute] int id)
        //{
        //    // return employees.First(x => x.Id == id);         //This will throw an exception if the employee with the specified id is not found
        //    return employees.FirstOrDefault(x => x.Id == id);   //This will return null if the employee with the specified id is not found
        //}

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
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            if(employee.Name == "")
            {
                return BadRequest("Name is Required");    // 400 Bad Request
            }
            if(employee.Id == 1 || employee.Id == 2 || employee.Id == 3)
            {
                return Conflict("Employee already Exist");   // 409 Conflict
            }
            employees.Add(employee);
            return Created("", employee);

        }

        [HttpPut("UpdateEmployee/{id}")]
        public IActionResult UpdateEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
          if(employee.Name == "")
            {
                return BadRequest("Name is Required");    // 400 Bad Request
            }
          if(employee.Id != 1 || employee.Id != 2 || employee.Id != 3)
            {
                return NotFound($"Employee with ID not found.");  // 404 not found
            }
            return Ok("Employee Updated Succesfully");    // 200 Ok

        }

        [HttpPatch("PatchEmployee/{id}")]
        public IActionResult PatchEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            if (employee.Name == "")
            {
                return BadRequest("Name is Required");    // 400 Bad Request
            }
            if (employee.Id != 1 || employee.Id != 2 || employee.Id != 3)
            {
                return NotFound($"Employee with ID not found.");  // 404 not found
            }
            return Ok("Employee Updated Succesfully");    // 200 Ok
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            if (id != 1 || id != 2 || id != 3)
            {
                return NotFound($"Employee with ID not found.");  // 404 not found
            }
            return NoContent();    // 204 no Content
        }

        [HttpGet("TemporaryEmployeeURL")]
        public IActionResult TemporaryEmployeeURL()
        {
            return Redirect("/api/EmployeeV4/GetAllEmployees");    // 302
        }

        [HttpGet("CheckLogin")]
        public IActionResult CheckLogin(bool isLoggedIn)
        {
            if(isLoggedIn == false)
            {
                return Unauthorized("Please Log In");    // 401 Unauthorized
            }
            return Ok("Log In Successfull");     // 200 Ok
        }

        [HttpGet("CheckPermission")]
        public IActionResult CheckPermission(bool isAdmin)
        {
            if(isAdmin == false)
            {
                return StatusCode(403, "Access Denied");    // 403 Forbidden
            }
            return StatusCode(200, "Access Granted");    // 200 Ok
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