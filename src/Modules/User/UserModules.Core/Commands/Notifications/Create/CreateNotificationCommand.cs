using AutoMapper;
using Common.Application;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModules.Data;
using UserModules.Data.Entities.Notifications;

namespace UserModules.Core.Commands.Notifications.Create;

public class CreateNotificationCommand : IBaseCommand
{
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public string Title { get; set; }

}
public class CreateNotificationCommandHandler : IBaseCommandHandler<CreateNotificationCommand>
{
    private readonly UserContexts _userContexts;
    private readonly IMapper _mapper;

    public CreateNotificationCommandHandler(UserContexts userContexts, IMapper mapper)
    {
        _userContexts = userContexts;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<UserNotification>(request);

        _userContexts.Add(model);
        await _userContexts.SaveChangesAsync();
        return OperationResult.Success();
    }
}
public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();
    }
}
