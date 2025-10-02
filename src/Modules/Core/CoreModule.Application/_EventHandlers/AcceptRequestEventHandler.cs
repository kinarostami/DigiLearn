using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Domain.Teacher.Events;
using MediatR;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application._EventHandlers;

public class AcceptRequestEventHandler : INotificationHandler<AcceptTeacherRequestEvent>
{
    private readonly IEventBus _eventBus;

    public AcceptRequestEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(AcceptTeacherRequestEvent notification, CancellationToken cancellationToken)
    {
        _eventBus.Publish(new NewNotificationIntegrationEvent()
        {
            CreationDate = notification.CreationDate,
            Title = "درخواست مدرسی شما تایید شد",
            Message = $"تبریک ! , پنل مدرسی شما در این لینک در دسترس است <hr/><a href='/profile/teacher/courses'>ورود</a> ",
            UserId = notification.UserId
        },"",Exchanges.NotificationExchange,ExchangeType.Fanout);
        await Task.CompletedTask;
    }
}
