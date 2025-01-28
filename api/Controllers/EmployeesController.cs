using api.DAL;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Newtonsoft.Json;
using MongoDB.Driver.Core.Configuration;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeesController : Controller
    {
        /* Should I move the sql connection outside of every call?
           Or should I put it into a function that gets called w/ every HTTP call
           ^^ this seems safe but also super slow...
         */

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

        [HttpPut(Name = "CreateEmployee")]
        // Should I break employee into separate url-based args and then create an Employee_SQL object in the function body?
        public HttpResponseMessage CreateEmployee(int id, string name, string email, string occupation)
        {
            // Create employee object
            Employee_SQL employee = new Employee_SQL();
            employee.Id = id;
            employee.Name = name;
            employee.Email = email;
            employee.Occupation = occupation;
            //Console.WriteLine(employee);

            SqlConnection connection = DatabaseConnect.SQLServerConnect(DatabaseConnect.sqlConnStr);

            /// Source: https://stackoverflow.com/questions/19956533/sql-insert-query-using-c-sharp
            using (connection)
            {
                String query = "INSERT INTO Master.dbo.Employees (id,Name,Email,Occupation) VALUES (@id,@Name,@Email, @Occupation)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", employee.Id);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Occupation", employee.Occupation);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                    
                    connection.Close();
                }
            }
            ///

            return new HttpResponseMessage(new System.Net.HttpStatusCode()); // OK
        }

        /*[HttpPost(Name = "CreateEmployee")]
        public Task<ActionResult<Employee_SQL>> CreateEmployee()
        {

        }*/



    }
}
