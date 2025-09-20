using Common.Application;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModules.Data;

namespace UserModules.Core.Commands.Users.Edit;

public class EditUserCommand : IBaseCommand
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string? Email { get; set; }
}
public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly UserContexts _userContexts;

    public EditUserCommandHandler(UserContexts userContexts)
    {
        _userContexts = userContexts;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userContexts.Users.FirstOrDefaultAsync(x => x.Id == request.UserId,cancellationToken);
        if (user == null)
            return OperationResult.NotFound();

        user.Family = request.Family;
        user.Name = request.Name;
        if (string.IsNullOrWhiteSpace(request.Email) == false)
        {
            if (await EmailIsDuplicated(request.Email))
            {
                return OperationResult.Error("ایمیل وارد شده تکراری است");
            }
            user.Email = request.Email.ToLower();
        }

        await _userContexts.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();

    }
    private async Task<bool> EmailIsDuplicated(string email)
    {
        return await _userContexts.Users.AnyAsync(f => f.Email == email.ToLower());
    }
}
public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(f => f.Family)
            .NotEmpty()
            .NotNull();
    }
}
