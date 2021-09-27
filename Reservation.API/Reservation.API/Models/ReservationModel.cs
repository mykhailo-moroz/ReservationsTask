using System;
using System.ComponentModel.DataAnnotations;

namespace Reservation.API.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string ClientName { get; set; }
    }
}