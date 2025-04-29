using System.ComponentModel.DataAnnotations;

namespace DatingApp.Domain.Dto
{
    public class RegisterUserDto : LoginUserDto
    {

        [Required]
        public string KnownAs {  get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string DateOfBirth { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

    }
}
