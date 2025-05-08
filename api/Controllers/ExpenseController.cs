using api.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using api.Models.SQL;


namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ExpenseController : Controller
    {
        DatabaseConnect databaseConnect = new DatabaseConnect();

        // GET: /<controller>/
        [HttpGet(Name = "GetExpenses")]
        public ActionResult<List<Expense>> GetExpenses()
        {
            SqlConnection connection = new SqlConnection(databaseConnect.sqlConnStr);
            List<Expense> expenses = new List<Expense>();
            try
            {
                SqlCommand command = new SqlCommand("select * from master.dbo.Expenses", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Expense expense = new Expense();
                    expense.Id = reader.GetInt32(0);
                    expense.Value = reader.GetDecimal(1);
                    expense.Description = reader.GetString(2);
                    expenses.Add(expense);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return expenses;
        }
    }
}

