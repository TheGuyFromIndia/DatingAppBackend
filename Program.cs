using DatingApp.Data;
using DatingApp.Infrastructure;
using DatingApp.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

UseMiddlewares(app);

await RunMigrations(app);

app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddService(builder.Configuration);
    builder.Services.RegisterDatabase(builder.Configuration);
    builder.Services.AddCors();
    builder.Services.AddAuthentification(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

static void UseMiddlewares(WebApplication app)
{
    app.UseMiddleware<ExceptionMiddleware>();

    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200/"));

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

}

static async Task RunMigrations(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();
        await Seed.SeedUsers(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger>();
        logger.LogError(ex, "An error occured");

    }
}