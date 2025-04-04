using RoleBaseAuthorization.Models;

namespace RoleBaseAuthorization.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);

    }
}
