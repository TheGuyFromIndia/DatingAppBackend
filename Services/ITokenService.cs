using DatingApp.Domain.Entity;

namespace DatingApp.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
