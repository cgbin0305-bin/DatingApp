using System.Text;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Put services every where before the builder.build() line code(before it is the config of app )
builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.IdentityServices(builder.Configuration);

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200")); // allow to send API server to the web

// using Authentication middleware before the MapController and after UseCors
app.UseAuthentication();// Do you have a valid token ?

app.UseAuthorization(); // Ok you have a valid token => what are you allowed to do?
app.MapControllers();

app.Run();
