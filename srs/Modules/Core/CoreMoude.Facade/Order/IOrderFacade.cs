using Common.Application;
using CoreModule.Application.Order.AddItem;
using CoreModule.Application.Order.FinallyOrder;
using CoreModule.Application.Order.RemoveItem;
using CoreModule.Query.Order._DTOs;
using CoreModule.Query.Order.GetCurrent;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Facade.Order;

public interface IOrderFacade
{
    Task<OperationResult> AddItem(AddOrderItemCommand command);
    Task<OperationResult> RemoveItem(RemoveOrderItemCommand command);
    Task<OperationResult> FinallyOrder(FinallyOrderCommand command);

    Task<OrderDto?> GetCurrentOrder(Guid orderId);
}
public class OrderFacade : IOrderFacade
{
    private readonly IMediator _mediator;

    public OrderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddItem(AddOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> FinallyOrder(FinallyOrderCommand command)
    {
        return await _mediator.Send(command); ;
    }

    public async Task<OrderDto?> GetCurrentOrder(Guid orderId)
    {
        return await _mediator.Send(new GetOrderCurrentQuery(orderId));
    }

    public async Task<OperationResult> RemoveItem(RemoveOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }
}