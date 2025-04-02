using api.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using api.Models.SQL;


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
        public ActionResult<List<Employee>> GetEmployees()
        {
            SqlConnection connection = DatabaseConnect.SQLServerConnect(DatabaseConnect.sqlConnStr);

            // Create a list of Client_SQLs
            List<Employee> employees = new List<Employee>();
            string employeesJson = "";

            // Source: https://stackoverflow.com/questions/21709305/how-to-directly-execute-sql-query-in-c
            SqlCommand command = new SqlCommand("select * from master.dbo.Employees", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Employee client = new Employee();
                    client.Id = reader.GetInt32(0);
                    client.Name = reader.GetString(1);
                    client.Email = reader.GetString(2);
                    client.Occupation = reader.GetString(3);
                    employees.Add(client); 
                }
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
            Employee employee = new Employee();
            employee.Name = name;
            employee.Email = email;
            employee.Occupation = occupation;

            SqlConnection connection = DatabaseConnect.SQLServerConnect(DatabaseConnect.sqlConnStr);

            /// Source: https://stackoverflow.com/questions/19956533/sql-insert-query-using-c-sharp
            using (connection)
            {
                string query = "INSERT INTO Master.dbo.Employees (Name,Email,Occupation) VALUES (@Name,@Email, @Occupation)";

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

        [HttpPost(Name = "UpdateEmployee")]
        public IActionResult UpdateEmployee(int id, string? nameNew, string? emailNew, string? occupationNew)
        {
            try
            {
                // Find SQL object based on ID
                SqlConnection connection = DatabaseConnect.SQLServerConnect(DatabaseConnect.sqlConnStr);
                SqlCommand query = new SqlCommand("SELECT Id, Name, Email, Occupation " +
                    "from master.dbo.Employees where Id = " + id.ToString(), connection);
                Employee client = new Employee();
                connection.Open();
                SqlDataReader reader = query.ExecuteReader();

                // Grab the employee record to update
                while (reader.Read())
                {
                    client.Id = reader.GetInt32(0);
                    client.Name = reader.GetString(1);
                    client.Email = reader.GetString(2);
                    client.Occupation = reader.GetString(3);
                }
                Console.WriteLine(client);

                /* Instances of the String (alias "string") class are objects (reference type),
                    and can't be checked w/ Nullable<T>.HasValue. */
                if (!string.IsNullOrEmpty(nameNew))
                {
                    client.Name = nameNew;
                }
                if (!string.IsNullOrEmpty(emailNew))
                {
                    client.Email = emailNew;
                }
                if (!string.IsNullOrEmpty(occupationNew))
                {
                    client.Occupation = occupationNew;
                }

                // Update the record in the DB
                string updateQuery = "update master.dbo.Employees " +
                    "set Name = @Name, Email = @Email, Occupation = @Occupation " +
                    "where Id = @Id";

                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Name", client.Name);
                    updateCommand.Parameters.AddWithValue("@Email", client.Email);
                    updateCommand.Parameters.AddWithValue("@Occupation", client.Occupation);
                    updateCommand.Parameters.AddWithValue("@Id", id);

                    updateCommand.ExecuteNonQuery();
                }

                connection.Close();
                
                // Should I also return the updated Employee record?
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                //throw e??

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete(Name = "DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            // Find SQL object based on ID
            SqlConnection connection = DatabaseConnect.SQLServerConnect(DatabaseConnect.sqlConnStr);
            SqlCommand query = new SqlCommand("DELETE from master.dbo.Employees " +
                "where Id = " + id.ToString(), connection);
            connection.Open();
            query.ExecuteNonQuery();
            connection.Close();

            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
