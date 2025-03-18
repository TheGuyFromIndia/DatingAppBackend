using DatingApp.Domain.Entity;
using DatingApp.Domain.Extensions;
using DatingApp.Domain.Dto;
using DatingApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [Authorize]
    public class UsersController(IUserRepository userRepository) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetUsersAsync()
        {
            return Ok((await userRepository.GetUsersAsync()).Select(x => x.ToMember()));
        }

        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Member>> GetUserAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null) { 
                return NotFound();
            }

            return Ok(user.ToMember());
        }


        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<Member>> GetUserAsync(string username)
        {
            var user = await userRepository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToMember());
        }
    }
}
