using AirlineTicketSystem.Data.Constants;
using AirlineTicketSystem.Data.Entities;
using AirlineTicketSystem.Models.User;
using AirlineTicketSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineTicketSystem.Services
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

        public async Task SeedUserWithRoleAsync(string email, string password, UserRolesEnum role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = CreateUser(email);
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

           return userManager.Users.Select(user => new UserViewModel(user.Id, user.Email, user.Name, string.Join(", ", userManager.GetRolesAsync(user).Result)));
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = new List<UserViewModel>();

            var userRoles = Enum.GetValues(typeof(UserRolesEnum));
            foreach (var role in userRoles)
            {
                var usersInRoleEntities = await userManager.GetUsersInRoleAsync(role.ToString());
                var usersInRole = usersInRoleEntities
                    .Select(user => new UserViewModel(user.Id, user.Email, user.Name, role.ToString()));

                users.AddRange(usersInRole);
            }

            return users;
        }


        private ApplicationUser CreateUser(string email)
        {
            return new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = email,
                Name = email
            };
        }
    }
}
