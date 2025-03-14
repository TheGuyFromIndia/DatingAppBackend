using DatingApp.Domain.Dto;

namespace DatingApp.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
