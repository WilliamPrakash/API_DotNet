using api.Models;
using api.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EmployeeController : Controller
{
    private readonly SQL_DBContext _dbContext;

    public EmployeeController(SQL_DBContext dbContext)
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
    public async void UpdateEmployee() // Recieves a JSON Employee model
    {
        /* TODO: wrap the json deserializing logic in a try/catch */
        // Get data from request body
        string rawContent = string.Empty;
        using (var reader = new StreamReader(Request.Body,
            encoding: Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false))
        {
            rawContent = await reader.ReadToEndAsync();
        }
        Employee? employeeUpdates = JsonSerializer.Deserialize<Employee>(rawContent);

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

    /* is "async void" a problem? https://stackoverflow.com/questions/55543595/cannot-access-a-disposed-object-a-common-cause-of-this-error-is-disposing-a-con */
    [HttpPost]
    public async void CreateEmployee() // Recieves a JSON Employee model
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
    public void DeleteEmployee(int Id)
    {
        using (_dbContext)
        {
            Employee ?employeeToDelete = _dbContext.Employees.SingleOrDefault(x => x.Id == Id);
            if (employeeToDelete != null)
            {
                _dbContext.Employees.Remove(employeeToDelete);
                _dbContext.SaveChanges();
            }
            
        }
    }

}
