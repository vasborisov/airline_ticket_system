using AirlineTicketSystem.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineTicketSystem.Data.Constants;

namespace AirlineTicketSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task SeedUserWithRoleAsync(string email, string password, UserRolesEnum role);

        IEnumerable<UserViewModel> GetAll();

        Task<IEnumerable<UserViewModel>> GetAllAsync();


    }
}
