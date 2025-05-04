using AirlineTicketSystem.Models.Passenger;
using AirlineTicketSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTicketSystem.Controllers
{
    public class PassengerController : Controller
    {
        private readonly IPassengerService passengerService;

        public PassengerController(IPassengerService passengerService)
        {
            this.passengerService = passengerService;
        }

       /* public IActionResult Index()
        {
            var flights = flightService.LoadAllFlights();

            return View(flights);
        }*/

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Operator")]
        public async Task<IActionResult> CreateAsync(CreatePassengerViewModel createPassengerViewModel)
        {
            await passengerService.RegisterPassangerAsync(createPassengerViewModel);

            return View();
        }
    }
}