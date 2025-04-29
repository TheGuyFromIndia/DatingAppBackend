using System.ComponentModel.DataAnnotations;

namespace DatingApp.Domain.Dto
{
    public class LoginUserDto
    {
        [Required]
        public required string UserName { get; set; } = string.Empty;
        [Required]
        [StringLength(8, MinimumLength =4)]
        public required string Password { get; set; } = string.Empty;
    }
}
