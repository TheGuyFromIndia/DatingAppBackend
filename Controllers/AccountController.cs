using DatingApp.Domain.Dto;
using DatingApp.Domain.Entity;
using DatingApp.Infrastructure;
using DatingApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Controllers
{
    public class AccountController(DataContext dbContext, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<AuthUserDto>> Register(RegisterUserDto registerUser)
        {
            if (!await ValidateUser(registerUser.UserName))
            {
                return BadRequest($"User with username {registerUser.UserName} already exists");
            }

            using var hmac = new HMACSHA512();

            var x = DateTime.Parse(registerUser.DateOfBirth).Date.Date;

            var user = new Domain.Entity.User
            {
                Name = registerUser.UserName.ToLower(),
                Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password)),
                Salt = hmac.Key,
                KnownAs = registerUser.KnownAs,
                Gender = registerUser.Gender,
                City = registerUser.City,
                Country = registerUser.Country,
                DateOfBirth = DateOnly.Parse(registerUser.DateOfBirth),
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Ok(new AuthUserDto
            {
                UserName = user.Name,
                Token = tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url!
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthUserDto>> LoginAsync(Domain.Dto.LoginUserDto login)
        {
            var userEntity = await dbContext.Users.Include(x=>x.Photos).FirstOrDefaultAsync(x => x.Name == login.UserName.ToLower());

            if (userEntity == null)
            {
                return Unauthorized();
            }

            using var hmac = new HMACSHA512(userEntity.Salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != userEntity.Hash[i])
                {
                    return Unauthorized();
                }
            }

            return Ok(new AuthUserDto
            {
                UserName = userEntity.Name,
                Token = tokenService.CreateToken(userEntity),
                PhotoUrl = userEntity.Photos.FirstOrDefault(x => x.IsMain)?.Url
            });
        }

        private async Task<bool> ValidateUser(string username)
        {
            return await dbContext.Users
                .FirstOrDefaultAsync(x => x.Name.ToLower() == username.ToLower()) == null;
        }
    }
}
