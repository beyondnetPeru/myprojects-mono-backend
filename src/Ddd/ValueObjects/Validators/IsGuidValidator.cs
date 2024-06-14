using FluentValidation;

namespace Ddd.ValueObjects.Validators
{
    public static class IsGuidValidator
    {
        public static IRuleBuilderOptions<T, string> IsGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => Guid.TryParse(x, out _))
                .WithMessage("Value must be a valid GUID.");
        }
    }
}
