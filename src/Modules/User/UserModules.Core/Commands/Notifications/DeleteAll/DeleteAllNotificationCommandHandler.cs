using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModules.Core.Commands.Notifications.DeleteAll;
using UserModules.Data;

namespace UserModules.Core.Commands.Notifications.Delete;

public class DeleteAllNotificationCommandHandler : IBaseCommandHandler<DeleteAllNotificationCommand>
{
    private readonly UserContexts _userContexts;

    public DeleteAllNotificationCommandHandler(UserContexts userContexts)
    {
        _userContexts = userContexts;
    }
    public async Task<OperationResult> Handle(DeleteAllNotificationCommand request, CancellationToken cancellationToken)
    {
        var model = await _userContexts.Notifications.Where(x => x.UserId == request.UserId).ToListAsync();
        if (model == null)
            return OperationResult.NotFound();
        if (model.Any())
        {
            _userContexts.Notifications.RemoveRange(model);
            await _userContexts.SaveChangesAsync();
        }
        
        return OperationResult.Success();
    }
}
