using DatingApp.Infrastructure;
using DatingApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

UseMiddlewares(app);

app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddService();
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