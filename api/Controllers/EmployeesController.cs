using api.DAL;
using api.Models;
using api.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
        public void UpdateEmployee(Employee updatedEmployee)
        {
            //Employee employee = new Employee();
            using (_dbContext)
            {
                //employee = _dbContext.Employees.Single(employee => employee.Id == emp.Id);
                _dbContext.Employees.Update(updatedEmployee);
                _dbContext.SaveChanges();
            }
        }

        [HttpPost]
        public void CreateEmployee()
        {
            Employee newEmployee = new Employee();
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
