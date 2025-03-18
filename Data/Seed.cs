using DatingApp.Domain.Dto;
using DatingApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DatingApp.Data;
public class Seed
{
    public static async Task SeedUsers(DataContext context)
    {
        if (await context.Users.AnyAsync()) return;

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserSeedData.Json");
        var userData = await File.ReadAllTextAsync(filePath);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var users = JsonSerializer.Deserialize<List<User>>(userData, options);

        if (users != null)
        {
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.Name = user.Name.ToLower();
                user.Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.Salt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}