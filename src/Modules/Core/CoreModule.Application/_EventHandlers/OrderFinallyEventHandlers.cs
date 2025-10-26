using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Domain.Order.Events;
using MediatR;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application._EventHandlers;

public class OrderFinallyEventHandlers : INotificationHandler<OrderFinallyEvent>
{
    private readonly IEventBus _EventBus;

    public OrderFinallyEventHandlers(IEventBus eventBus)
    {
        _EventBus = eventBus;
    }

    public async Task Handle(OrderFinallyEvent notification, CancellationToken cancellationToken)
    {
        _EventBus.Publish(new NewNotificationIntegrationEvent()
        {
            Message = "فاکتور شما با موفقیت پرداخت شد",
            Title = "پرداخت موفق",
            UserId = notification.UserId,
        },null,Exchanges.NotificationExchange,ExchangeType.Fanout);
        await Task.CompletedTask;
    }
}
