using System.ComponentModel.DataAnnotations;

namespace DatingApp.Domain.Dto
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required byte[] Hash { get; set; }
        public required byte[] Salt { get; set; }
    }
}
