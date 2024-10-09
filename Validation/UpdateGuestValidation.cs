using FluentValidation;
using Guest.BAL.Commands;
using Guest.BAL.Interface;
using TestAPI_TEE.Model;

namespace Guest.API.Validation
{
    public class UpdateGuestValidation: AbstractValidator<UpdateGuestCommand>
    {
        private readonly IGuestRepository _guestRepository;

        public UpdateGuestValidation(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;

            RuleFor(guest=> guest.Id).NotEmpty().WithMessage("Id is mandatory");
            RuleFor(guest => guest.Firstname).NotEmpty().WithMessage("Firstname is required");
            RuleFor(guest => guest.PhoneNumbers).Must(x => x != null && x.Length > 0)
                .WithMessage("Atleast one Phone number is required");
            RuleFor(guest => guest.PhoneNumbers).Must(x => x.Any())
            .WithMessage("Duplicate Phone numbers in the list are not allowed")
            .When(x => _guestRepository.IsPhoneDuplicate(x.PhoneNumbers).Result);
        }
    }
}
