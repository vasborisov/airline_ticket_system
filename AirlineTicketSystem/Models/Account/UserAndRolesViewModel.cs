using System.ComponentModel.DataAnnotations;
using AirlineTicketSystem.Data.Entities;

namespace AirlineTicketSystem.Models
{
    public class UserAndRolesViewModel
    {
        public ApplicationUser User { get; set; }
        public List<string> Roles { get; set; }

        // Constructor that initializes the Roles property to an empty list
        public UserAndRolesViewModel()
        {
            Roles = new List<string>();
        }
    }
}
