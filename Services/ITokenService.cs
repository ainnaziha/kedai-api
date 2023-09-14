using KedaiAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace KedaiAPI.Services
{
    public interface ITokenService
    {
        Task<TokenResult> CreateTokenAsync(User user, UserManager<User> userManager);
    }
}
