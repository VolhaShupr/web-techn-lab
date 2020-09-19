using Microsoft.AspNetCore.Identity;

namespace lab.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] AvatarImage { get; set; }
    }
}
