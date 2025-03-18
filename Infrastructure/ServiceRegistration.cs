using DatingApp.Data;
using DatingApp.Infrastructure.Interfaces;
using DatingApp.Services;

namespace DatingApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
