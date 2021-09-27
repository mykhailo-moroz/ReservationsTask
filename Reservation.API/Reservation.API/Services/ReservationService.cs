using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Reservation.API.Exceptions;
using Reservation.API.Models;
using Reservation.Data.Models;
using Reservation.Data.Services;

namespace Reservation.API.Services
{
    public sealed class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly IOptions<ReservationSettings> _options;

        public ReservationService(IReservationRepository repository, IOptions<ReservationSettings> options)
        {
            _repository = repository;
            _options = options;
        }

        public async Task<IEnumerable<ReservationModel>> GetReservationList()
        {
            var reservations = await _repository.GetReservationsAsync();
            return reservations.Select(r => new ReservationModel
            {
                Id = r.Id,
                ClientName = r.ClientName,
                Date = r.Date
            });
        }

        public async Task CreateNewReservation(ReservationModel reservationModel)
        {
            if (reservationModel == null)
            {
                throw new ArgumentNullException(paramName:nameof(reservationModel));
            }

            var reservationsCount = await _repository.GetReservationsCountAsync();

            if (reservationsCount >= _options.Value.Capacity)
            {
                throw new ReservationMaxReachedException(reservationModel, _options.Value.Capacity);
            }

            await _repository.InsertReservationAsync(new ReservationDBModel
            {
                ClientName = reservationModel.ClientName,
                Date = reservationModel.Date
            });
        }
    }
}