
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection IdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // inside here we can specify all of the rules about how our user VALIDATE
                    //Validate the issuer signing key
                    // Sever is going to check the token signing key
                    ValidateIssuerSigningKey = true, // our token has been signed by the issuer => This is essential
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(config["TokenKey"])), // need Encoding to a byte[] from secrete token
                    ValidateIssuer = false, // the issuer of token is our API server. We need to pass that information down with the token in order to validate it 
                    ValidateAudience = false
                };

            });
            return services;
        }
    }
}