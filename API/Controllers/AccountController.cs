
using System.Text;
using System.Security.Cryptography;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("register")] // POST: api/account/register?username=Dave&password=pwd
        /*
        In the past, we would need to tell the framework where to look for the thing that we are passing. Like tell framework where to get the object from req body by add the attribute named [FromBody] at the previous's object
        Another thing that API controller attribute give us is, it's automatically going to check validation
        */
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) // api controller attributes will binds the object in the body req were send from client inside the method. But the prop names in object need to match exactly with what we have in DTO 
        {
            //1. Check user if it exist
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            //A password were send from API server that is a string => we want to hash this password using a hashing algorithm
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)), // ComputeHash method is return the byte[] but the password is a string so that we need to getByte of it by using Encoding.UTF8.GetBytes
                //Method GetBytes cant convert a null string into Byte[]
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(); // when we changing the DB such as update or add or delete => must have SaveChangeAsync() method to save this change
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            // should you method firstOrDefault rather than Find (cause Find method is best for find based on primary key) 
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null)
            {
                return Unauthorized("invalid username");
            }
            //check password by reverse to what we did before computed hash
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
            }
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower()); // user could be enter the username by lower case or upper case randomly => so that change all to lower case then compare
        }
    }
}