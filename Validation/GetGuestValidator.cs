using FluentValidation;
using Guest.BAL.Interface;
using Guest.BAL.Query;

namespace Guest.API.Validation
{
    public class GetGuestValidator : AbstractValidator<GetGuestQuery>
    {
        private readonly IGuestRepository _guestRepository;

        public GetGuestValidator(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;

            RuleFor(guest => guest.Id).NotEmpty().WithMessage("Id is mandatory");
        }
    }
}
