using AirlineTicketSystem.Entities;
using AirlineTicketSystem.Models.Flight;
using AirlineTicketSystem.Repositories.Interfaces;
using AirlineTicketSystem.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace AirlineTicketSystem.Tests.Services
{
    public class LoadAllFlights_Test
    {
        [Fact]
        public void LoadAllFlights_ReturnsCorrectFlightViewModels()
        {
            // Arrange
            var expectedFlightEntities = new List<Flight>
            {
                new Flight(1, "New York", "Los Angeles", 5, 200, 150),

                new Flight(2, "London", "Paris", 3, 150, 100)
            };

            var flightRepositoryMock = new Mock<IFlightRepository>();
            flightRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedFlightEntities);

            var flightService = new FlightService(flightRepositoryMock.Object, Mock.Of<IPassengerRepository>());

            // Act
            var result = flightService.LoadAllFlightsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                item =>
                {
                    Assert.Equal(expectedFlightEntities[0].Id, item.Id);
                    Assert.Equal(expectedFlightEntities[0].DepartureCity, item.DepartureCity);
                    Assert.Equal(expectedFlightEntities[0].ArrivalCity, item.ArrivalCity);
                    Assert.Equal(expectedFlightEntities[0].Duration, item.Duration);
                    Assert.Equal(expectedFlightEntities[0].Price, item.Price);
                    Assert.Equal(expectedFlightEntities[0].Capacity, item.Capacity);
                },
                item =>
                {
                    Assert.Equal(expectedFlightEntities[1].Id, item.Id);
                    Assert.Equal(expectedFlightEntities[1].DepartureCity, item.DepartureCity);
                    Assert.Equal(expectedFlightEntities[1].ArrivalCity, item.ArrivalCity);
                    Assert.Equal(expectedFlightEntities[1].Duration, item.Duration);
                    Assert.Equal(expectedFlightEntities[1].Price, item.Price);
                    Assert.Equal(expectedFlightEntities[1].Capacity, item.Capacity);
                });
        }
    }
}
