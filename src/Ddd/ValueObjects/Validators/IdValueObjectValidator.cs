using FluentValidation;

namespace Ddd.ValueObjects.Validators
{
    public class IdValueObjectValidator : AbstractValidator<IdValueObject>
    {
        public IdValueObjectValidator()
        {
            RuleFor(x => x.GetValue())
                .NotEmpty()
                .WithMessage("Value cannot be empty.");

            RuleFor(x => x.GetValue()).IsGuid().WithMessage("Value for Id have to be a Guid");
        }
    }
}
