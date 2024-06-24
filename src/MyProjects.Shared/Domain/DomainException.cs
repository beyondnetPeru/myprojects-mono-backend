using FluentValidation.Results;

namespace MyProjects.Shared.Domain
{
    [Serializable]
    public class DomainException : Exception
    {
        private IReadOnlyCollection<ValidationFailure> validationFailures;

        public DomainException(IReadOnlyCollection<ValidationFailure> validationFailures)
        {
            this.validationFailures = validationFailures;
        }
    }
}