using Common.Application;
using CoreModule.Domain.Order.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Order.RemoveItem;

public record RemoveOrderItemCommand(Guid UserId,Guid Id) : IBaseCommand;

public class RemoveOrderItemCommandHanlder : IBaseCommandHandler<RemoveOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public RemoveOrderItemCommandHanlder(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetCurrentOrderByUserId(request.UserId);
        if (order == null)
            return OperationResult.NotFound();

        order.RemoveItem(request.Id);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}
