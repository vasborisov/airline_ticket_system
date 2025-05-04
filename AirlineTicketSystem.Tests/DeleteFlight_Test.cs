using AirlineTicketSystem.Repositories.Interfaces;
using AirlineTicketSystem.Services;
using Moq;
using Xunit;

namespace AirlineTicketSystem.Tests.Services
{
    public class DeleteFlight_Test
    {
        [Fact]
        public void DeleteFlight()
        {
            // Arrange
            var flightRepositoryMock = new Mock<IFlightRepository>();
            var passengerRepositoryMock = new Mock<IPassengerRepository>();
            var flightService = new FlightService(flightRepositoryMock.Object, passengerRepositoryMock.Object);
            int flightId = 1;

            // Act
            flightService.DeleteFlight(flightId);

            // Assert
            flightRepositoryMock.Verify(repo => repo.Delete(flightId), Times.Once);
        }
    }
}
