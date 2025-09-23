using Common.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Core.Queries._DTOs;
using UserModules.Core.Commands.Notifications.Create;
using UserModules.Core.Commands.Notifications.Delete;
using UserModules.Core.Commands.Notifications.DeleteAll;
using UserModules.Core.Commands.Notifications.Seen;
using UserModules.Core.Queries.Notifications.GetFilter;

namespace UserModules.Core.Services;

public interface INotificationFacade
{
    Task<OperationResult> Create(CreateNotificationCommand command);
    Task<OperationResult> Delete(DeleteNotificationCommand command);
    Task<OperationResult> DeleteAll(DeleteAllNotificationCommand command);
    Task<OperationResult> SeenNotification(SeenNotificationCommand command);

    Task<NotificationFilterResult> GetByFilter(NotificationFilterParams filterParams);
}
public class NotificationFacade : INotificationFacade
{
    private readonly IMediator _mediator;

    public NotificationFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteAll(DeleteAllNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<NotificationFilterResult> GetByFilter(NotificationFilterParams filterParams)
    {
        return await _mediator.Send(new GetNotificationsByFilterQuery(filterParams));
    }

    public async Task<OperationResult> SeenNotification(SeenNotificationCommand command)
    {
        return await _mediator.Send(command);
    }
}
