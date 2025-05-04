
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketSystem.Entities
{
    public class FlightPassenger
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public virtual Flight? Flight { get; set; }
        public int PassengerId { get; set; }
        public virtual Passenger? Passenger { get; set; }
    }
}