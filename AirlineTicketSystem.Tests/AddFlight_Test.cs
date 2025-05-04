using AirlineTicketSystem.Entities;
using AirlineTicketSystem.Models.Flight;
using AirlineTicketSystem.Repositories.Interfaces;
using AirlineTicketSystem.Services;
using Moq;

namespace AirlineTicketSystem.Tests.Services
{
    public class AddFlight_Test
    {
        [Fact]
        public void Add_ValidFlight()
        {
            // Arrange
            var flightRepositoryMock = new Mock<IFlightRepository>();
            var passengerRepositoryMock = new Mock<IPassengerRepository>();
            var flightService = new FlightService(flightRepositoryMock.Object, passengerRepositoryMock.Object);
            var createFlightViewModel = new CreateFlightViewModel
            {
                Id = 1,
                DepartureCity = "New York",
                ArrivalCity = "Los Angeles",
                Duration = 5,
                Price = 200,
                Capacity = 150
            };

            // Act
            flightService.AddFlight(createFlightViewModel);

            // Assert
            flightRepositoryMock.Verify(repo => repo.Add(It.IsAny<Flight>()), Times.Once);
        }
    }
}
