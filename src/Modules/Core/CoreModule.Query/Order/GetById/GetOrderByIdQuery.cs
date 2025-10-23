using Common.Domain.Repository;
using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Order._DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Order.GetById;

public record GetOrderByIdQuery(Guid id) : IQuery<OrderDto?>;

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly QueryContext _context;

    public GetOrderByIdQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Course)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.id);

        return OrdreMapper.MapOrder(order);
    }
}
