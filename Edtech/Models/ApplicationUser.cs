using Microsoft.AspNetCore.Identity;

namespace Edtech.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public string Address { get; set; }
    }
}
