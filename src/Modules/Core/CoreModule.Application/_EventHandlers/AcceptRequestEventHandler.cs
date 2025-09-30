using CoreModule.Domain.Teacher.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application._EventHandlers;

public class AcceptRequestEventHandler : INotificationHandler<AcceptTeacherRequestEvent>
{
    public async Task Handle(AcceptTeacherRequestEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
public class RejectRequestEventHandler : INotificationHandler<RejectTeacherRequestEvent>
{
    public async Task Handle(RejectTeacherRequestEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
