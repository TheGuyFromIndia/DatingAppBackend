using DatingApp.Domain.Entity;
using DatingApp.Infrastructure;
using DatingApp.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class UserRepository(DataContext context) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByUserNameAsync(string name)
        {
            return await context.Users.Include(x => x.Photos).SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await context.Users.Include(x => x.Photos).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}
