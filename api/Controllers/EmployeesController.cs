using api.DAL;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
//using Newtonsoft.Json;


namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeesController : Controller
    {
        /* Should I move the sql connection outside of every call?
           Or should I put it into a function that gets called w/ every HTTP call
           ^^ this seems safe but also super slow... */

        [HttpGet(Name = "GetEmployees")]
        public ActionResult<List<Employee_SQL>> GetEmployees()
        {
            SqlConnection connection = DatabaseConnect.SQLServerConnect(DatabaseConnect.sqlConnStr);

            // Create a list of Client_SQLs
            List<Employee_SQL> employees = new List<Employee_SQL>();
            string employeesJson = "";

            // Source: https://stackoverflow.com/questions/21709305/how-to-directly-execute-sql-query-in-c
            SqlCommand command = new SqlCommand("select * from master.dbo.Employees", connection);
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

        /* Getting args: https://stackoverflow.com/questions/25385559/rest-api-best-practices-args-in-query-string-vs-in-request-body
            Usually the content body is used for the data that is supposed to be uploaded/downloaded to/from
            the server ,and the query parameters are used to specify the exact data requested. */
        [HttpPut(Name = "CreateEmployee")]
        public HttpResponseMessage CreateEmployee(string name, string email, string occupation)
        {
            // Create employee object
            Employee_SQL employee = new Employee_SQL();
            //employee.Id = id;
            employee.Name = name;
            employee.Email = email;
            employee.Occupation = occupation;

            SqlConnection connection = DatabaseConnect.SQLServerConnect(DatabaseConnect.sqlConnStr);

            /// Source: https://stackoverflow.com/questions/19956533/sql-insert-query-using-c-sharp
            using (connection)
            {
                String query = "INSERT INTO Master.dbo.Employees (Name,Email,Occupation) VALUES (@Name,@Email, @Occupation)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@id", employee.Id);
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
            return new HttpResponseMessage(new System.Net.HttpStatusCode()); // OK
        }

        /*[HttpPost(Name = "UpdateEmployee")]
        public Task<ActionResult<Employee_SQL>> UpdateEmployee()
        {

        }*/



    }
}
