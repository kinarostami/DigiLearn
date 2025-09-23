using Common.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModules.Data;

namespace UserModules.Core.Commands.Notifications.Seen;

public record SeenNotificationCommand(Guid NotificationId) : IBaseCommand;

public class SeenNotificationCommandHandler : IBaseCommandHandler<SeenNotificationCommand>
{
    private readonly UserContexts _userContexts;

    public SeenNotificationCommandHandler(UserContexts userContexts)
    {
        _userContexts = userContexts;
    }

    public async Task<OperationResult> Handle(SeenNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _userContexts.Notifications
            .FirstOrDefaultAsync(x => x.Id == request.NotificationId && x.IsSeen == false);

        if (notification == null)
        {
            return OperationResult.NotFound();
        }

        notification.IsSeen = true;
        _userContexts.Update(notification);
        await _userContexts.SaveChangesAsync();
        return OperationResult.Success();
    }
}
