using CollaboratorRegisterApi.Models.Requests;
using FluentValidation;

namespace CollaboratorRegisterApi.Validators
{
    public class CollaboratorAddValidator : AbstractValidator<CollaboratorAddRequest>
    {
        public CollaboratorAddValidator()
        {
            RuleFor(e => e.Name).NotNull().NotEmpty().WithMessage(string.Format(Resource.Required, Resource.Name));
            RuleFor(e => e.Mail).NotNull().NotEmpty().WithMessage(string.Format(Resource.Required, Resource.Mail));
            RuleFor(e => e.PlateNumber).NotNull().NotEmpty().WithMessage(string.Format(Resource.Required, Resource.PlateNumber));
            RuleFor(e => e.Password).NotNull().NotEmpty().WithMessage(string.Format(Resource.Required, Resource.Password));
        }
    }
}
