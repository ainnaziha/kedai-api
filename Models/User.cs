using Microsoft.AspNetCore.Identity;

namespace KedaiAPI.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
