using API.Controllers;
using API.Models;
using API.Models.SQL;
using System.Text.Json;
using System.Diagnostics;
using System.Text;
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
    public async Task UpdateEmployee_Test() // HTTP PUT
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5000/api/Employee/UpdateEmployee");
            string jsonEmployee = JsonSerializer.Serialize(new Employee()
            {
                Id = 1016,
                Name = "Updated via unit test",
                Email = "Updated via unit test",
                Occupation = "Updated via unit test"
            });

            using StringContent jsonContent = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            using HttpResponseMessage res = await client.PutAsync(client.BaseAddress, jsonContent);

            Assert.IsTrue(res.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }

    [Test]
    public async Task CreateEmployee_Test() // HTTP POST
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5000/api/Employee/CreateEmployee");
            string jsonEmployee = JsonSerializer.Serialize(new Employee()
            {
                Name = "Sfyaisodfkawjneriwahu",
                Email = "GmailYahooAOL",
                Occupation = "beekeeper"
            });

            // Create request
            // source: https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient
            using StringContent jsonContent = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            using HttpResponseMessage res = await client.PostAsync(client.BaseAddress, jsonContent);

            Assert.IsTrue(res.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }

    [Test]
    public void DeleteEmployee_Test() // HTTP DELETE
    {

        Assert.Pass();
    }
    #endregion

    [TearDown]
    public void TearDown()
    {
        apiExecutable.Kill();
        apiExecutable.Dispose();
        empController.Dispose();
    }
}

/*FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
{
    new KeyValuePair<string, string>("Name", "Sfyaisodfkawjneriwahu"),
    new KeyValuePair<string, string>("Email", "GmailYahooAOL"),
    new KeyValuePair<string, string>("Occupation", "beekeeper")
});*/