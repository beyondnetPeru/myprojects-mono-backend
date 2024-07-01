using FluentValidation;

namespace Ddd.ValueObjects.Validators
{
    public class StringNotEmptyValueObjectValidator : AbstractValidator<StringNotEmptyValueObject>
    {
        public StringNotEmptyValueObjectValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty();
            RuleFor(x => x.Value)
                .NotNull();
        }
    }
}
