
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //Connect to DB
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();
            //Add Transient => very short lived service => Token service would be created and disposed with the req as soon as it's used and finished
            // Add Singleton => Will create when the application first start and never disposed until the application has closed down 
            // we just need the token service when we need to create a token => should use ADdScoped<>
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // we need to tell services where our mapping profiles are(but we only got a single project(=> we have single assembly) => auto mapper is already)
            /*
             type of authentication scheme going to use inside this Services is: JwtBearerDefault
             => need to install package named Microsoft.AspNetCore.Authentication.JwtBearer (by Microsoft)
            */
            return services;
        }
    }
}