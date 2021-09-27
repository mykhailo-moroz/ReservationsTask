using System;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Reservation.API.Exceptions;
using Reservation.API.Models;
using Reservation.API.Services;
using Reservation.Data.Services;

namespace Reservation.Tests
{
    [TestFixture]
    public class ReservationServiceTests
    {
        private IReservationService _reservationService;

        private Mock<IReservationRepository> _repositoryMock;
        private Mock<IOptions<ReservationSettings>> _optionsMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IReservationRepository>();
            _optionsMock = new Mock<IOptions<ReservationSettings>>();
            _optionsMock.Setup(o => o.Value)
                .Returns(new ReservationSettings
                {
                    Capacity = 2
                });

            _reservationService = new ReservationService(_repositoryMock.Object, _optionsMock.Object);
        }

        [Test]
        public void CreateNewReservation_ShouldNotAllowCreationOfMoreReservationThanCapacity()
        {
            _repositoryMock.Setup(r => r.GetReservationsCountAsync()).ReturnsAsync(2);

            _reservationService.Invoking(rs => rs.CreateNewReservation(It.IsAny<ReservationModel>()))
                .Should().ThrowAsync<ReservationMaxReachedException>();
        }

        [Test]
        public void CreateNewReservation_ShouldNotAllowCreationNullReservation()
        {
            _repositoryMock.Setup(r => r.GetReservationsCountAsync()).ReturnsAsync(2);

            _reservationService.Invoking(rs => rs.CreateNewReservation(null))
                .Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
