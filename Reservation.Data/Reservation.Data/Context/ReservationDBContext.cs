using Microsoft.EntityFrameworkCore;
using Reservation.Data.Models;

namespace Reservation.Data.Context
{
    public class ReservationDBContext : DbContext
    {
        public ReservationDBContext(DbContextOptions<ReservationDBContext> options) : base(options) { }

        public DbSet<ReservationDBModel> Reservations { get; set; }
    }
}