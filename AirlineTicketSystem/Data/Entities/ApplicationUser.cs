using Microsoft.AspNetCore.Identity;

namespace AirlineTicketSystem.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { set; get; }
    }
}
