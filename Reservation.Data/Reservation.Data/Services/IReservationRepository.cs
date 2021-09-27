using System.Collections.Generic;
using System.Threading.Tasks;
using Reservation.Data.Models;

namespace Reservation.Data.Services
{
    public interface IReservationRepository
    {
        Task<IEnumerable<ReservationDBModel>> GetReservationsAsync();
        Task<int> GetReservationsCountAsync();
        Task<ReservationDBModel> GetReservationByIDAsync(int reservationId);
        Task InsertReservationAsync(ReservationDBModel reservation);
        Task DeleteReservationAsync(int reservationId);
        Task UpdateReservationAsync(ReservationDBModel reservation);
        Task SaveAsync();
    }
}