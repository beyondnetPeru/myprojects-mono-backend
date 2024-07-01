using FluentValidation;


namespace Ddd.ValueObjects.Validators
{
    public class IdValueObjectCannotBeEmptyRule : AbstractValidator<IdValueObject>
    {
        public IdValueObjectCannotBeEmptyRule()
        {
            RuleFor(x => x.Value).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.Value).MaximumLength(36).WithMessage("Id cannot be longer than 36 characters");
            RuleFor(x => x.Value).MinimumLength(36).WithMessage("Id cannot be shorter than 36 characters");
            RuleFor(x => x.Value).Matches("^[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}$").WithMessage("Id is not in the correct format");
        }        
    }
}
