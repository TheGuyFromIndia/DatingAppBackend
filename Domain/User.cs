using System.ComponentModel.DataAnnotations;

namespace DatingApp.Domain
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
