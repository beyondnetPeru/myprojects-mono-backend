using FluentValidation;

namespace Ddd.ValueObjects.Validators
{
    public class StringNotEmptyValueObjectValidator : AbstractValidator<string>
    {
        public StringNotEmptyValueObjectValidator()
        {
            RuleFor(x => x)
                .NotEmpty();
            RuleFor(x => x)
                .NotNull();
        }
    }
}
