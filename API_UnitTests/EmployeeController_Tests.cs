using API.Controllers;
using API.Models;
using API.Models.SQL;
//using System.Net.Http; ?
using System.Text.Json;
using System.Diagnostics;
using System.Net.Http.Json;

namespace API_UnitTests;

public class Tests
{
    private EmployeeController empController;
    readonly string dir = Directory.GetCurrentDirectory();
    private Process apiExecutable;

    [SetUp]
    public void Setup()
    {
        apiExecutable = Process.Start(dir + "//API.exe");
        /* ^Now listening on: http://localhost:5000 */

        SQL_DBContext dBContext = new SQL_DBContext();
        this.empController = new EmployeeController(dBContext);
    }

    #region Tests
    [Test]
    public void GetEmployees_Test() // HTTP GET
    {
        List<Employee> employees = this.empController.GetEmployees();
        Assert.IsTrue(employees.Count > 0);
    }

    [Test]
    public void UpdateEmployee_Test() // HTTP PUT
    {
        

        Assert.Pass();
    }

    [Test]
    public async Task CreateEmployee_Test() // HTTP POST
    {
        using (HttpClient client = new HttpClient())
        {
            // https://stackoverflow.com/questions/15176538/net-httpclient-how-to-post-string-value
            client.BaseAddress = new Uri("http://localhost:5000");
            /*FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "Sfyaisodfkawjneriwahu"),
                new KeyValuePair<string, string>("Email", "GmailYahooAOL"),
                new KeyValuePair<string, string>("Occupation", "beekeeper")
            });*/
            Employee testEmployee = new Employee()
            {
                Name = "Sfyaisodfkawjneriwahu",
                Email = "GmailYahooAOL",
                Occupation = "beekeeper"
            };
            string jsonEmployee = JsonSerializer.Serialize(testEmployee);
            var response = await client.PostAsJsonAsync("/api/Employee/CreateEmployee", jsonEmployee);
            //var content = new FormUrlEncodedContent(testEmployee);
            //var response = await client.PostAsync("/api/Employee/CreateEmployee", content);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine();
        }
        
        Assert.Fail();
    }

    [Test]
    public void DeleteEmployee_Test() // HTTP DELETE
    {

        Assert.Pass();
    }
    #endregion

    [Test]
    [TearDown]
    public void TearDown()
    {
        apiExecutable.Kill();
        apiExecutable.Dispose();
        empController.Dispose();
    }
}