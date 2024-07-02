using FluentValidation;

namespace MyProjects.Domain.ReleaseAggregate.Validators
{
    public class GoLiveValidator : AbstractValidator<GoLive>
    {
        public GoLiveValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Value).Must(x => x > System.DateTime.Now).WithMessage("GoLive date must be in the future");
            RuleFor(x => x.Value).Must(x => x.DayOfWeek != System.DayOfWeek.Saturday && x.DayOfWeek != System.DayOfWeek.Sunday).WithMessage("GoLive date must be a working day");
        }
    }
}
