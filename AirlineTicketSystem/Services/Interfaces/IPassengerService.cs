using AirlineTicketSystem.Models.Passenger;

namespace AirlineTicketSystem.Services.Interfaces
{
    public interface IPassengerService
    {
        Task RegisterPassangerAsync(CreatePassengerViewModel passenger);
    }
}