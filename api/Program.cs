using System.Collections.Generic;
using api.DAL;
using MongoDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure routing format
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) { }

// For static files
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

// Grab database credentials/connection strings
GrabDatabaseCredentials auth = new GrabDatabaseCredentials();
Dictionary<string,string>? credentials = auth.OpenLocalAuthFile();

// If no DB credentials are found, shut down the application.
if (credentials == null) { System.Environment.Exit(1); } // How to exit a Console app

string mongoPwd = "";
string sqlConnStr = "";

if (credentials.Count != 0)
{
    for (int i = 0; i < credentials.Keys.Count; i++)
    {
        if (credentials.ElementAt(i).Key == "MongoDB")
        {
            mongoPwd = credentials.ElementAt(i).Value;
        }
        else if (credentials.ElementAt(i).Key == "SQLServer")
        {
            sqlConnStr = credentials.ElementAt(i).Value;
        }
    }
}

DatabaseConnect dbConn = new DatabaseConnect();

// MongoDB
/*if (mongoPwd != "")
{
    var mongoConnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings:MongoDB").Value;
    var mongoConnStrPwd = mongoConnStr.Replace("<password>", mongoPwd);
    dbConn.MongoDBConnect(mongoConnStrPwd);
}*/

// SQL Server
if (sqlConnStr != "")
{
    dbConn.SQLServerConnect(sqlConnStr);
}


app.MapControllers();

app.Run();
