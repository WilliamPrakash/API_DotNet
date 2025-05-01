using api.Models;
using api.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;


namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeesController : Controller
    {
        private readonly SQL_DBContext _dbContext;

        public EmployeesController(SQL_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (_dbContext)
            {
                employees = _dbContext.Employees.ToList();
            }

            return employees;
        }

        [HttpPut]
        public async void UpdateEmployee()
        {
            // Get data from request body
            string rawContent = string.Empty;
            using (var reader = new StreamReader(Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false))
            {
                rawContent = await reader.ReadToEndAsync();
            }
            Employee ?employeeUpdates = JsonSerializer.Deserialize<Employee>(rawContent);

            if (employeeUpdates != null)
            {
                using (_dbContext)
                {
                    Employee employee = _dbContext.Employees.Single(employee => employee.Id == employeeUpdates.Id);
                    employee.Name = employeeUpdates.Name != null ? employeeUpdates.Name : employee.Name;
                    employee.Email = employeeUpdates.Email != null ? employeeUpdates.Email : employee.Name;
                    employee.Occupation = employeeUpdates.Occupation != null ? employeeUpdates.Occupation : employee.Occupation;

                    _dbContext.Employees.Update(employee);
                    _dbContext.SaveChanges();
                }
            }
            else
            {
                // error handling
            }
        }

        [HttpPost]
        public async void CreateEmployee()
        {
            string rawContent = string.Empty;
            using (var reader = new StreamReader(Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false))
            {
                rawContent = await reader.ReadToEndAsync();
            }
            Employee? newEmployee = JsonSerializer.Deserialize<Employee>(rawContent);
            
            if (newEmployee != null)
            {
                using (_dbContext)
                {
                    _dbContext.Employees.Add(newEmployee);
                    _dbContext.SaveChanges();
                }
            }
            else
            {
                // error handling
            }

        }

        [HttpDelete]
        public void DeleteEmployee(int id)
        {
            using (_dbContext)
            {
                //_dbContext.Employees.ExecuteDelete(employee => employee.Id == id);
            }
        }

    }
}
