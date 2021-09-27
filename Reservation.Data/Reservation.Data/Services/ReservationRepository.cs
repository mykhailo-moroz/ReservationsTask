using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reservation.Data.Context;
using Reservation.Data.Models;

namespace Reservation.Data.Services
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationDBContext _context;

        public ReservationRepository(ReservationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReservationDBModel>> GetReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<int> GetReservationsCountAsync()
        {
            return await _context.Reservations.CountAsync();
        }

        public async Task<ReservationDBModel> GetReservationByIDAsync(int reservationId)
        {
            return await _context.Reservations.FindAsync(reservationId);
        }

        public async Task InsertReservationAsync(ReservationDBModel reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await SaveAsync();
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservationToDelete = await _context.Reservations.FindAsync(reservationId);
            _context.Reservations.Remove(reservationToDelete);
        }

        public async Task UpdateReservationAsync(ReservationDBModel reservation)
        {
            _context.Reservations.Update(reservation);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}