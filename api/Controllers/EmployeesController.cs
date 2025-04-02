using api.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using api.Models.SQL;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeesController : Controller
    {


        public List<Employee> GetEmployees()
        {


            return new List<Employee>();
        }


    }
}
