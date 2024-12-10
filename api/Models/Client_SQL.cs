namespace api.Models
{
    public class Client_SQL
    {
        public int Id { get; set; }
        public string Name { get; set; } // nvarchar(50)
        public string Email { get; set; } // nvarchar(50)
        public string Occupation { get; set; } // nvarchar(50)
    }
}
