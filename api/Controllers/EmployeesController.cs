using api.DAL;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeesController : Controller
    {


        [HttpGet(Name = "GetEmployees")]
        public ActionResult<List<Employee_SQL>> GetEmployees()
        {
            SqlConnection connection = DatabaseConnect.SQLServerConnect(DatabaseConnect.sqlConnStr);

            // Create a list of Client_SQLs
            List<Employee_SQL> employees = new List<Employee_SQL>();
            string employeesJson = "";

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
                //employeesJson = JsonConvert.SerializeObject(employees);
                //return employeesJson;
                return employees;
            }
            finally
            {
                reader.Close();
            }
        }



    }
}
