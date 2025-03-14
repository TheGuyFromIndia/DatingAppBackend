using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure
{
    public static class DatabaseRegistration
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration config) 
        {
            return services.AddDbContext<DataContext>(opt =>{opt.UseSqlite(config.GetConnectionString("SqlConnectionString"));});
        }
    }
}
