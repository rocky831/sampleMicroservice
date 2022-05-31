using AVEVA.PA.MicroserviceTemplate.Application.Commands;
using FluentValidation;

namespace Microservice.Application.Validation.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            this.RuleFor(x => x.Project == null ? "" : x.Project.Name).NotEmpty().WithMessage("Project Empty")
                .MaximumLength(20).WithMessage("Length");
            this.RuleFor(x => x.Project == null ? "" : x.Project.Description).NotEmpty().WithMessage("Description Empty").MaximumLength(20)
                .WithMessage("Length");
        }
    }
}
