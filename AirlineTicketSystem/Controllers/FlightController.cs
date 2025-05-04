using AirlineTicketSystem.Models;
using AirlineTicketSystem.Models.Flight;
using AirlineTicketSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AirlineTicketSystem.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightService flightService;

        public FlightController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        public async Task<IActionResult> Index(string searchDepartureCity)
        {
            var flights = await flightService.LoadAllFlightsAsync();

            // Apply filtering if the searchDepartureCity is provided
            if (!String.IsNullOrEmpty(searchDepartureCity))
            {
                flights = flights.Where(f => f.DepartureCity.Equals(searchDepartureCity, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Return the filtered list to the view
            return View(flights);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreateFlightViewModel flightViewModel)
        {

            if (!ModelState.IsValid) {
                return View(flightViewModel);
            } 

            flightService.AddFlight(flightViewModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var flight = await flightService.GetFlight(id);

            // Check if the flight exists
            if (flight == null)
            {
                // If the flight doesn't exist
                return RedirectToAction("Index", "Flight");
            }

            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAsync(EditFlightViewModel flight)
        {
            await flightService.EditAsync(flight);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Reset()
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> BookSeat(int id)
        {
            var flight = await flightService.GetFlight(id, true);

            // Check if the flight exists
            if (flight == null)
            {
                // If the flight doesn't exist
                return RedirectToAction("Index", "Flight");
            }

            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [ActionName("BookSeatAsync")]
        public async Task<IActionResult> BookSeatAsync(BookSeatViewModel bookSeatModel)
        {
            await flightService.BookSeatAsync(bookSeatModel.FlightId, bookSeatModel.PassengerId);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var flight = await flightService.GetFlight(id);

            if (flight == null)
            {
                // If flight does not exist
                return RedirectToAction("Index", "Flight");
            }

            await flightService.DeleteFlightAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}