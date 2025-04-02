using Microsoft.EntityFrameworkCore;
using api.Models.SQL;
//using System.Runtime.InteropServices;


namespace api.Models
{
    public class SQL_DBContext : DbContext
    {
        // Tables
        public DbSet<Employee> Employees { get; set; }
        // To Do: Add Expenses?

        // Database provider
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

        }

    }
}
