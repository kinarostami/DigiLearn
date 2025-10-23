using Common.Application;
using CoreModule.Domain.Order.DomainServices;
using CoreModule.Domain.Order.Models;
using CoreModule.Domain.Order.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Order.AddItem;

public record AddOrderItemCommand(Guid UserId,Guid CourseId) : IBaseCommand;

public class AddOrderItemCommandHanlder : IBaseCommandHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDomainService _orderDomainService;

    public AddOrderItemCommandHanlder(IOrderRepository orderRepository, IOrderDomainService orderDomainService)
    {
        _orderRepository = orderRepository;
        _orderDomainService = orderDomainService;
    }

    public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetCurrentOrderByUserId(request.UserId);
        if (order == null)
        {
            var newOrder = new Domain.Order.Models.Order(request.UserId);
            await newOrder.AddItem(request.CourseId,_orderDomainService);
            _orderRepository.Add(newOrder);
        }
        else
        {
            await order.AddItem(request.CourseId,_orderDomainService);
        }

        await _orderRepository.Save();
        return OperationResult.Success();
    }
}

