using System;
using FluentValidation;
using Reservation.API.Models;

namespace Reservation.API.Validators
{
    public class ReservationValidator : AbstractValidator<ReservationModel>
    {
        public ReservationValidator()
        {
            RuleFor(x => x.Date)
                .NotNull().GreaterThan(DateTime.Now);

            RuleFor(x => x.ClientName)
                .NotEmpty();
        }
    }
}