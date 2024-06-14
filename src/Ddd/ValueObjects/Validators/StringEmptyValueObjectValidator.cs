using FluentValidation;

namespace Ddd.ValueObjects.Validators
{
    public class StringEmptyValueObjectValidator : AbstractValidator<string>
    {
        public StringEmptyValueObjectValidator()
        {
            RuleFor(value => value)
                .NotNull()    
                .WithMessage("Value cannot be NULL.");
        }
    }
}
