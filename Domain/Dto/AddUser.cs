using System.ComponentModel.DataAnnotations;

namespace DatingApp.Domain.Dto
{
    public class AddUser
    {
        [Required]
        public  string UserName { get; set; } = string.Empty;
        [Required]
        [StringLength(8, MinimumLength = 6)]
        public  string Password { get; set; } = string.Empty;
    }
}
