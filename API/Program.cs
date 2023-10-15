using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Put services every where before the builder.build() line code(before it is the config of app )
builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.IdentityServices(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(builder => builder.AllowAnyHeader()
                              .AllowAnyMethod()
                              .WithOrigins("https://localhost:4200")); // allow to send API server to the web

// using Authentication middleware before the MapController and after UseCors
app.UseAuthentication();// Do you have a valid token ?

app.UseAuthorization(); // Ok you have a valid token => what are you allowed to do?
app.MapControllers();


// Dotnet CLI can help us to create DB (can use in code at Program.cs)
using var scope = app.Services.CreateScope(); // this is going to give us access to all of the services that we have inside program class
var services = scope.ServiceProvider;
try
{
  var context = services.GetRequiredService<DataContext>();
  await context.Database.MigrateAsync();
  await Seed.SeedUser(context);
}
catch (System.Exception ex)
{

  var logger = services.GetService<ILogger<Program>>();
  logger.LogError(ex, "An error occurred during migration");
}
app.Run();
