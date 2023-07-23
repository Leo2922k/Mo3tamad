using System.Security.Cryptography;
using System.Text;
using API.Date;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
            
        }


        [HttpPost("register")] // POST: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) {

            if (await UserExists(registerDto.Username)) 
                return BadRequest ("Username is taken");

            // hashing: hmac
            // using: to clear the memory, automatic disposal
            using var hmac = new HMACSHA512();

            var user = new AppUser {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            // for db
            await _context.SaveChangesAsync();

            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task <ActionResult<UserDto>> Login (LoginDto loginDto) {
            
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            // FirstOrDefaultAsync - First - SingleOrDefault

            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i=0; i< ComputeHash.Length; i++) 
                if (ComputeHash[i] != user.PasswordHash[i]) 
                    return Unauthorized ("Invalid Password"); 
         

            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };


        }

        // prevent repeat the username
        private async Task<bool> UserExists (string username) {

            // x: appuser
            // AnyAsync: Asynchronously determines whether any element of a sequence satisfies a condition
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());

        }
    }
}