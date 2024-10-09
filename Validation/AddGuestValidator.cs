using FluentValidation;
using Guest.BAL.Commands;
using Guest.BAL.Interface;
using TestAPI_TEE.Model;

namespace TestAPI_TEE.Validation
{
    public class AddGuestValidator: AbstractValidator<AddGuestCommand>
    {
        private readonly IGuestRepository _guestRepository;
        public AddGuestValidator(IGuestRepository guestRepository) 
        {
            _guestRepository = guestRepository;

            RuleFor(guest => guest.Firstname).NotEmpty().WithMessage("Firstname is required");
            RuleFor(guest => guest.PhoneNumbers).Must(x=>x!=null && x.Length > 0)
                .WithMessage("Atleast one Phone number is required");
            RuleFor(guest => guest.PhoneNumbers).Must(x => x.Any())
            .WithMessage("Duplicate Phone numbers in the list are not allowed")
            .When(x => _guestRepository.IsPhoneDuplicate(x.PhoneNumbers).Result);
        }
    }
}
