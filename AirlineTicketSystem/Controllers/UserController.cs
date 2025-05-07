using Airline_Ticket_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Airline_Ticket_System.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        //public IActionResult Index()
        //{
        //    var user = userService.GetAll();

        //    return View(user);
        //}

        public async Task<IActionResult> IndexAsync()
        {
            var user = await userService.GetAllAsync();

            return View(user);
        }
    }
}
