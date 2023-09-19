
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            //Create Claim: tao dac trung cua nguoi dung
            var claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            /*
                both System.IdentityModel.Tokens.Jwt and Microsoft.IdentityModel.JsonWebTokens do have the method is JwtRegisteredClaimNames
                => remove Microsoft.IdentityModel.JsonWebTokens;
            */
        };
            // Sign this token: encrypt the secret key
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            //Payload: 
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims), // info of user
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            // token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
