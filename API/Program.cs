using API.DAL;
using API.Models;

/* Initializes a new instance of the WebApplicationBuilder class with preconfigured defaults
   The WebApplication class is used to configure the HTTP pipeline and routes */
var builder = WebApplication.CreateBuilder(args);

/* Add services for controllers to the specified IServiceCollection. This method will not register services used for views or pages.
   This method configures the MVC services for the commonly used features with controllers for an API.
   This combines the effects of AddMvcCore, AddApiExplorer, AddAuthorization, AddCors, AddDataAnnotations */
builder.Services.AddControllers();
builder.Services.AddDbContext<SQL_DBContext>();
/* Source: https://stackoverflow.com/questions/71932980/what-is-addendpointsAPIexplorer-in-asp-net-core-6
   Is for Minimal APIs whereas AddApiExplorer requires MVC Core. For API Projects, AddControllers() calls AddApiExplorer on your behalf.
   With the introduction of Endpoint Routing, everything in the routing system boils down to an Endpoint. ASP.NET Core
   uses the Application Model, namely ApplicationModel, ControllerModel, and ActionModel to create Endpoint instances 
   and register them with the routing system. Minimal APIs, however, use a builder to directly create and register individual Endpoint instances.
 */
builder.Services.AddEndpointsApiExplorer(); // by the above source, I shouldn't need this line...
// Source: https://www.youtube.com/watch?v=m8224OuxzuY
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

var app = builder.Build();
app.UseCors("CorsPolicy");

// Configure routing format
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) { }

// For static files
app.UseDefaultFiles();
app.UseStaticFiles();

//app.UseHttpsRedirection();

app.UseAuthorization();

new DatabaseConnect();

app.MapControllers();

app.Run();
