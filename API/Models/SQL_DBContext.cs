using API.Models.SQL;
using API.DAL;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class SQL_DBContext : DbContext
{
    // Tables
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    // Database provider
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        DatabaseConnect databaseConnect = new DatabaseConnect();
        optionsBuilder.UseSqlServer(databaseConnect.sqlConnStr);
    }

}
