using System;
using Reservation.API.Models;

namespace Reservation.API.Exceptions
{
    public class ReservationMaxReachedException : Exception
    {
        public ReservationMaxReachedException(ReservationModel reservation, int maxCount) 
            : base($"Cannot create a reservation for {reservation.ClientName}. Max reservation count reached - {maxCount}")
        {
        }
    }
}