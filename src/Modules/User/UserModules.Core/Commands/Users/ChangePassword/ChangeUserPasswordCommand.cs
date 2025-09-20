using Common.Application;
using Common.Application.SecurityUtil;
using FluentValidation;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModules.Data;

namespace UserModules.Core.Commands.Users.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public Guid UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
public class ChangeUserPasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    private readonly UserContexts _userContexts;

    public ChangeUserPasswordCommandHandler(UserContexts userContexts)
    {
        _userContexts = userContexts;
    }

    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userContexts.Users.FirstOrDefaultAsync(f => f.Id == request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        if (Sha256Hasher.IsCompare(user.Password, request.CurrentPassword))
        {
            var hashedPassword = Sha256Hasher.Hash(request.NewPassword);
            user.Password = hashedPassword;
            _userContexts.Update(user);
            await _userContexts.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }
        return OperationResult.Error("کلمه عبور نامعتبر است");
    }
}
public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(f => f.CurrentPassword)
            .NotEmpty()
            .NotNull();

        RuleFor(f => f.NewPassword)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6);
    }
}