using System.Security.Claims;

namespace RoleBaseAuthorization.Extensions
{
    public static class ClaimsExtensions
    {
        public static string? GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
