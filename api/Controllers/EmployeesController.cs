using api.DAL;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : Controller
    {
        [HttpGet]
        public void GetEmployees()
        {
            SqlConnection connection = DatabaseConnect.SQLServerConnect(DatabaseConnect.sqlConnStr);

            // Create a list of Client_SQLs
            List<Employee_SQL> employees = new List<Employee_SQL>();

            // Source: https://stackoverflow.com/questions/21709305/how-to-directly-execute-sql-query-in-c
            SqlCommand command = new SqlCommand("select * from Master.dbo.Employees", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Employee_SQL client = new Employee_SQL();
                    client.Id = reader.GetInt32(0);
                    client.Name = reader.GetString(1);
                    client.Email = reader.GetString(2);
                    client.Occupation = reader.GetString(3);
                    employees.Add(client);
                }
            }
            finally
            {
                reader.Close();
            }
        }

        /*public IActionResult Index()
        {
            return View();
        }*/
    }
}
