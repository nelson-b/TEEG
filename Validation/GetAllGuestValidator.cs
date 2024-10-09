using FluentValidation;
using Guest.BAL.Interface;
using Guest.BAL.Query;

namespace Guest.API.Validation
{
    public class GetAllGuestValidator : AbstractValidator<GetAllGuestQuery>
    {
        public GetAllGuestValidator(IGuestRepository guestRepository)
        {
        }
    }
}
