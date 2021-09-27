using System.Collections.Generic;
using System.Threading.Tasks;
using Reservation.API.Models;

namespace Reservation.API.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationModel>> GetReservationList();

        Task CreateNewReservation(ReservationModel reservationModel);
    }
}