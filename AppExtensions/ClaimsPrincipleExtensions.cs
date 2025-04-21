using System.Security.Claims;

namespace DatingApp.AppExtensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            var username = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (username == null)
            {
                throw new Exception("JWT token invalid");
            }

            return username;
        }
    }
}
