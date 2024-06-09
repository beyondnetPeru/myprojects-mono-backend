using FluentValidation;
using MyProjects.Application.Dtos.Project;

namespace MyProjects.Application.Validators
{
    public  class CreateProjectValidatorDto : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectValidatorDto()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
