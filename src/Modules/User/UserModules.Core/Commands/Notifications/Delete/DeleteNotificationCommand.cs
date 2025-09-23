using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModules.Data;

namespace UserModules.Core.Commands.Notifications.Delete;

public record DeleteNotificationCommand(Guid NotificationId,Guid UserId) : IBaseCommand;

public class DeleteNotificationCommandHandler : IBaseCommandHandler<DeleteNotificationCommand>
{
    private readonly UserContexts _userContexts;

    public DeleteNotificationCommandHandler(UserContexts userContexts)
    {
        _userContexts = userContexts;
    }

    public async Task<OperationResult> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var model = await _userContexts.Notifications
            .FirstOrDefaultAsync(x => x.Id == request.NotificationId && 
            x.UserId == request.UserId,cancellationToken);
        if (model == null)
            return OperationResult.NotFound();

        _userContexts.Notifications.Remove(model);
        await _userContexts.SaveChangesAsync();
        return OperationResult.Success();
    }
}
