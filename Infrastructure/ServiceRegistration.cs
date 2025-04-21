using DatingApp.Data;
using DatingApp.Infrastructure.Interfaces;
using DatingApp.Services;

namespace DatingApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();

            return services;
        }
    }
}
