using AirlineTicketSystem.Entities;
using AirlineTicketSystem.Models.Passenger;
using AirlineTicketSystem.Repositories;
using AirlineTicketSystem.Services.Interfaces;

namespace AirlineTicketSystem.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly ApplicationDbContext _context;

        public PassengerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task RegisterPassangerAsync(CreatePassengerViewModel passengerCreateModel)
        {
            var newPassengerEntity = new Passenger(passengerCreateModel.FirstName, passengerCreateModel.LastName);

            _context.Passengers.AddAsync(newPassengerEntity);
            await _context.SaveChangesAsync();
        }
    }
}
