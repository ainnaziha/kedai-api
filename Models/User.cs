using Microsoft.AspNetCore.Identity;

namespace KedaiAPI.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
