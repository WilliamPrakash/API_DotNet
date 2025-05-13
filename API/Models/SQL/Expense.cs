
namespace api.Models.SQL;

public class Expense
{
	public int Id { get; set; } // [Id] INT IDENTITY (1, 1) NOT NULL
	public decimal Value { get; set; } // [Value] DECIMAL (18)  NOT NULL
	public string Description { get; set; } = string.Empty; // Description] NVARCHAR (50) NOT NULL
	// CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED ([Id] ASC)
    }

