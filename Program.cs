using _4LumenBackEnd.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer(); // Required for minimal APIs
builder.Services.AddSwaggerGen(); // Swagger generator

// Add DbContext for your PostgreSQL database
builder.Services.AddDbContext<XmlDetailsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DevConnection")));

// Configure Kestrel to listen on the correct port (DigitalOcean's default is 8080)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // Listen on port 8080 for HTTP traffic
});

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger middleware
    app.UseSwaggerUI(); // Enable Swagger UI
}

 app.UseHttpsRedirection();

app.UseCors(options =>
    options.AllowAnyOrigin() 
           .AllowAnyHeader()
           .AllowAnyMethod()
           );

app.UseAuthorization();

app.MapControllers();

app.Run();
