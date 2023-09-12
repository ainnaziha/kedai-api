using KedaiAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace KedaiAPI.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user, UserManager<User> userManager);
    }
}
