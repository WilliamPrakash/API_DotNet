using API.Controllers;
using API.Models;
using API.Models.SQL;
using System.Text.Json;
using System.Text.Unicode;
using System.Runtime.CompilerServices;
using System.Diagnostics;

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
        /* Now listening on: http://localhost:5000 */

        // Initialize Context + Controller
        SQL_DBContext dBContext = new SQL_DBContext();
        empController = new EmployeeController(dBContext);
    }

    #region Tests
    [Test]
    public async Task GetEmployees_Test() // HTTP GET
    {


        Assert.Fail();
    }

    [Test]
    public void UpdateEmployee_Test() // HTTP PUT
    {
        // Create test Employee to add to DB
        Employee employee = new Employee();
        employee.Name = "GetEmployees_Test() insert";
        employee.Email = "GetEmployees_Test() insert";
        employee.Occupation = "GetEmployees_Test() insert";
        /*using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://127.0.0.1:5215/api/Employee/UpdateEmployee");
            //client.DefaultRequestHeaders
        }*/

        Assert.Pass();
    }

    [Test]
    public void CreateEmployee_Test() // HTTP POST
    {

        Assert.Pass();
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