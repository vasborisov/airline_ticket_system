using AirlineTicketSystem.Entities;
using AirlineTicketSystem.Models.Passenger;
using AirlineTicketSystem.Repositories.Interfaces;
using AirlineTicketSystem.Services;
using Moq;
using Xunit;

namespace AirlineTicketSystem.Tests.Services
{
    public class RegisterPassenger_Test
    {
        [Fact]
        public void RegisterPassanger_AddsPassengerToRepository()
        {
            // Arrange
            var passengerCreateModel = new CreatePassengerViewModel
            {
                FirstName = "John",
                LastName = "Doe"
            };

            var passengerRepositoryMock = new Mock<IPassengerRepository>();
            var passengerService = new PassengerService(passengerRepositoryMock.Object);

            // Act
            passengerService.RegisterPassangerAsync(passengerCreateModel);

            // Assert
            passengerRepositoryMock.Verify(repo => repo.Add(It.IsAny<Passenger>()), Times.Once);
        }
    }
}
