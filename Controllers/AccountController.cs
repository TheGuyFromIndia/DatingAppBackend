using DatingApp.Domain.Dto;
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
        //[HttpPost("register")]
        //public async Task<ActionResult<User>> Register(AddUser addUser)
        //{
        //    if (!await ValidateUser(addUser.UserName))
        //    {
        //        return BadRequest($"User with username {addUser.UserName} already exists");
        //    }

        //    using var hmac = new HMACSHA512();

        //    var user = new User
        //    {
        //        Name = addUser.UserName.ToLower(),
        //        Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(addUser.Password)),
        //        Salt = hmac.Key,
        //    };

        //    dbContext.Users.Add(user);
        //    await dbContext.SaveChangesAsync();

        //    return Ok(user);
        //}

        [HttpPost("login")]
        public async Task<ActionResult<AuthUser>> LoginAsync(Login login)
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

            return Ok(new AuthUser
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
