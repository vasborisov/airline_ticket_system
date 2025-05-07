using Airline_Ticket_System.Data.Constants;
using Airline_Ticket_System.Data.Entities;
using Airline_Ticket_System.Models.User;
using Airline_Ticket_System.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airline_Ticket_System.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedUserWithRoleAsync(string email, string firstName, string familyName, string password, UserRolesEnum role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = CreateUser(email, firstName, familyName);
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync(role.ToString()))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                    }

                    await userManager.AddToRoleAsync(user, role.ToString());
                }
            }
        }

        public IEnumerable<UserViewModel> GetAll()
        {

           return userManager.Users.Select(user => new UserViewModel(user.Id, user.Email, user.FirstName, user.FamilyName, string.Join(", ", userManager.GetRolesAsync(user).Result)));
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = new List<UserViewModel>();

            var userRoles = Enum.GetValues(typeof(UserRolesEnum));
            foreach (var role in userRoles)
            {
                var usersInRoleEntities = await userManager.GetUsersInRoleAsync(role.ToString());
                var usersInRole = usersInRoleEntities
                    .Select(user => new UserViewModel(user.Id, user.Email, user.FirstName, user.FamilyName, role.ToString()));

                users.AddRange(usersInRole);
            }

            return users;
        }


        private ApplicationUser CreateUser(string email, string firstName, string familyName)
        {
            return new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = email,
                FirstName = firstName,
                FamilyName = familyName
            };
        }
    }
}
