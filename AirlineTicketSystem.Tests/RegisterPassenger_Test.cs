using Airline_Ticket_System.Entities;
using Airline_Ticket_System.Models.Passenger;
using Airline_Ticket_System.Repositories.Interfaces;
using Airline_Ticket_System.Services;
using Moq;
using Xunit;

namespace Airline_Ticket_System.Tests.Services
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
