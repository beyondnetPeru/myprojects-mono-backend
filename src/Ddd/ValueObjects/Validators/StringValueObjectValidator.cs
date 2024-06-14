using FluentValidation;

namespace Ddd.ValueObjects.Validators
{
    public class StringValueObjectValidator : AbstractValidator<string>
    {
        public StringValueObjectValidator()
        {
            RuleFor(x => x)                
                .NotNull()
                .WithMessage("Value cannot be null.");
        }
    }
}
