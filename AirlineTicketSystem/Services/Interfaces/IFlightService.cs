using AirlineTicketSystem.Models.Flight;
using System.Collections.Generic;

namespace AirlineTicketSystem.Services.Interfaces
{
    public interface IFlightService
    {
        Task AddFlight(CreateFlightViewModel flightCreateModel);
        Task<FlightViewModel?> GetFlight(int id, bool withPassengerList = false);

        Task EditAsync(EditFlightViewModel flight);

        Task DeleteFlightAsync(int id);

        Task<IEnumerable<FlightViewModel>> LoadAllFlightsAsync();
        void Search();
        Task BookSeatAsync(int flightId, int passengerId);
        void CancelBookedSeat();
        void RegisterPassanger();
    }
}
