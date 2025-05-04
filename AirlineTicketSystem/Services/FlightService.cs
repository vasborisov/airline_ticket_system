using AirlineTicketSystem.Entities;
using AirlineTicketSystem.Models.Flight;
using AirlineTicketSystem.Models.Passenger;
using AirlineTicketSystem.Repositories;
using AirlineTicketSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AirlineTicketSystem.Services
{
    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext _context;

        public FlightService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFlight(CreateFlightViewModel flightCreateModel)
        {
            var newFlightEntity = new Flight(
                flightCreateModel.Id,
                flightCreateModel.DepartureCity,
                flightCreateModel.ArrivalCity,
                flightCreateModel.Duration,
                flightCreateModel.Price,
                flightCreateModel.Capacity);

            _context.Flights.AddAsync(newFlightEntity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(EditFlightViewModel flight)
        {
            Flight flightEntity = new Flight(
                flight.Id,
                flight.DepartureCity, 
                flight.ArrivalCity,
                flight.Duration,
                flight.Price, 
                flight.Capacity
            );

            _context.Flights.Update(flightEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<FlightViewModel?> GetFlight(int id, bool withPassengerList)
        {
            var flight = await _context.Flights
                .FirstOrDefaultAsync(f => f.Id == id);
            if (flight == null) return null;

            var passengerViewModels = new List<PassengerViewModel>();

            if (withPassengerList)
            {
                if (withPassengerList)
                {
                    var passengers = await _context.Passengers.ToListAsync();
                    passengerViewModels = passengers.Select(p => new PassengerViewModel(
                        p.Id,
                        p.FirstName,
                        p.LastName
                    )).ToList();
                }
            }

                return new FlightViewModel(
                    flight.Id,
                    flight.DepartureCity,
                    flight.ArrivalCity,
                    flight.Duration,
                    flight.Price,
                    flight.Capacity,
                    flight.Capacity > 0 ? false : true,
                    passengerViewModels
                );
        }


        public async Task DeleteFlightAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
        }

        public async Task BookSeatAsync(int flightId, int passengerId)
        {
            var flight = await _context.Flights.FindAsync(flightId);
            var passenger = await _context.Passengers.FindAsync(passengerId);

            if (flight == null || passenger == null) return;

            // Assuming flight booking logic here
            // Decrease capacity or any other required logic
            flight.Capacity -= 1;

            var flightPassenger = new FlightPassenger
            {
                FlightId = flightId,
                PassengerId = passengerId
            };

            _context.FlightPassengers.Add(flightPassenger);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FlightViewModel>> LoadAllFlightsAsync()
        {
            var flights = await _context.Flights.ToListAsync();
            return flights.Select(f => new FlightViewModel(
                f.Id,
                f.DepartureCity,
                f.ArrivalCity,
                f.Duration,
                f.Price,
                f.Capacity,
                f.Capacity > 0 ? false : true
            ));
        }

        public void RegisterPassanger()
        {
            throw new System.NotImplementedException();
        }

        public void CancelBookedSeat()
        {
            throw new System.NotImplementedException();
        }

        public void Search()
        {
            throw new System.NotImplementedException();
        }
    }
}
