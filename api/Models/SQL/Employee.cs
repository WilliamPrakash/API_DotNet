
namespace api.Models.SQL;

public class Employee
{
    public int Id { get; set; } // [int] IDENTITY(1,1) NOT NULL
    public string? Name { get; set; } // [nvarchar](50) NULL
    public string? Email { get; set; } // [nvarchar](50) NULL
    public string? Occupation { get; set; } // [nvarchar](50) NULL
}
