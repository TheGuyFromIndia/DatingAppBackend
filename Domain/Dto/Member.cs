namespace DatingApp.Domain.Dto
{
    public class Member
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string KnownAs { get; set; }

        public string? PhotoUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public required string Gender { get; set; }
        public string? Introduction { get; set; }
        public string? Interests { get; set; }
        public string? LookingFor { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo> { };
    }
}
