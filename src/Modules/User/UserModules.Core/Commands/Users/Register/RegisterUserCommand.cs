﻿using Common.Application;
using Common.Application.SecurityUtil;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UserModules.Data;
using UserModules.Data.Entities.Users;

namespace UserModules.Core.Commands.Users.Register;

public class RegisterUserCommand : IBaseCommand<Guid>
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}
public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand, Guid>
{
    private readonly UserContexts _context;

    public RegisterUserCommandHandler(UserContexts context)
    {
        _context = context;
    }

    public async Task<OperationResult<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(f => f.PhoneNumber == request.PhoneNumber))
        {
            return OperationResult<Guid>.Error("شماره تلفن تکراری است");
        }
        var user = new User()
        {
            PhoneNumber = request.PhoneNumber,
            Password = Sha256Hasher.Hash(request.Password),
            Avatar = "default.png",
            Id = Guid.NewGuid()
        };
        _context.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return OperationResult<Guid>.Success(user.Id);
    }
}
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(r => r.Password)
            .NotEmpty()
            .NotNull().MinimumLength(6);
    }
}
