using FluentValidation;

namespace UserModules.Core.Commands.Roles.Create;

class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3).WithMessage("نقش باید بشتر از 3 کاراکتر باشد");
    }
}
