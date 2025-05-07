using Airline_Ticket_System.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;
using Airline_Ticket_System.Data.Constants;

namespace Airline_Ticket_System.Services.Interfaces
{
    public interface IUserService
    {
        Task SeedUserWithRoleAsync(string email, string firstName, string familyName, string password, UserRolesEnum role);

        IEnumerable<UserViewModel> GetAll();

        Task<IEnumerable<UserViewModel>> GetAllAsync();


    }
}
