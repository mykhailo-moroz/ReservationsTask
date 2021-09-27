using System;

namespace Reservation.Data.Models
{
    public class ReservationDBModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ClientName { get; set; }
    }
}