using DatingApp.Domain;
using DatingApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersControlller(DataContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            return Ok(await dbContext.Users.ToListAsync());
        }

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
