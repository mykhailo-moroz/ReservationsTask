using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Reservation.API.Exceptions;
using Reservation.API.Models;
using Reservation.API.Services;

namespace Reservation.API.Controllers
{
    /// <summary>
    /// Reservations controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(IReservationService reservationService, ILogger<ReservationsController> logger)
        {
            _reservationService = reservationService ??
                                  throw new ArgumentNullException(nameof(reservationService));
            _logger = logger ??
                      throw new ArgumentNullException(nameof(logger)); 
        }

        /// <summary>
        /// Provide a list of all reservations. ReservationModel must have a Date and client name.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("reservations")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("[GetReservation] Started!");
                var reservations = await _reservationService.GetReservationList();
                _logger.LogInformation($"[GetReservation] Finished! Reservations found: {reservations.Count()}");
                return new OkObjectResult(reservations);
            }
            catch (Exception e)
            {
                _logger.LogError("[GetReservation] Failed with error!", e);
                return new BadRequestResult();
            }
        }

        /// <summary>
        /// Try to create a new reservationModel. Input DTO must have the Date and client name.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("reservations")]
        public async Task<IActionResult> Post([FromBody] ReservationModel reservationModel)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            try
            {
                _logger.LogInformation($"[CreateReservation] Started! Creating reservation for {reservationModel.ClientName}: {reservationModel.Date}");
                await _reservationService.CreateNewReservation(reservationModel);
                _logger.LogInformation("[CreateReservation] Completed!");
                return new OkResult();
            }
            catch (ReservationMaxReachedException re)
            {
                _logger.LogError("[CreateReservation] Failed with error! Maximum count reached", re);
                return new StatusCodeResult((int)HttpStatusCode.Conflict);
            }
            catch (Exception e)
            {
                _logger.LogError("[CreateReservation] Failed with error!", e);
                return new BadRequestResult();
            }
        }
    }
}
