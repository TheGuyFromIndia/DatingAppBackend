namespace DatingApp.Domain.Dto
{
    public class AuthUser
    {
        public required string UserName { get; set; }
        public required string Token { get; set; }
        public string PhotoUrl { get; set; }
    }
}
