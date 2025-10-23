using Common.Application;
using CoreModule.Domain.Order.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Order.FinallyOrder;

public class FinallyOrderCommand : IBaseCommand
{
    public Guid OrderId { get; set; }
}
public class FinallyOrderCommandHandler : IBaseCommandHandler<FinallyOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public FinallyOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(FinallyOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId);
        if (order == null)
            return OperationResult.NotFound();

        order.FinallyOrder();
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}
