using FluentValidation;

namespace Ddd.ValueObjects.Validators
{
    public class IdValueObjectValidator : AbstractValidator<IdValueObject>
    {
        public IdValueObjectValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Value cannot be empty.");

            RuleFor(x => x.Value).IsGuid().WithMessage("Value for Id have to be a Guid");
        }
    }
}
