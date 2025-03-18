using DatingApp.AppExtensions;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.Domain.Dto
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public  byte[] Hash { get; set; } = [];
        public byte[] Salt { get; set; } = [];
        public DateOnly DateOfBirth { get; set; }
        public required string KnownAs { get; set; }
        public DateTime Created {  get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public required string Gender { get; set; }
        public string? Introduction { get; set; }
        public string? Interests { get; set; }
        public string? LookingFor { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo> { };

        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }
    }
}
