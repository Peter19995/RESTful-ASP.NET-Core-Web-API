using Microsoft.AspNetCore.Identity;

namespace MagicVill_VillaAPI.Models
{
    public class ApplicationUser : IdentityUser 
    {
        public string  Name { get; set; }   
    }
}
