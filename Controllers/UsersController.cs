using DatingApp.Domain.Dto;
using DatingApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    public class UsersController(DataContext dbContext) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            return Ok(await dbContext.Users.ToListAsync());
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> GetUserAsync(int id)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user == null) { 
                return NotFound();
            }

            return Ok(user);
        }
    }
}
