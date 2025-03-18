using DatingApp.Domain.Entity;

namespace DatingApp.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        void Update(User user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetUserByUserNameAsync(string name);
    }
}
