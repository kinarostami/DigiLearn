using Common.Query;
using CoreModule.Domain.Order.Models;
using CoreModule.Query._Data;
using CoreModule.Query.Order._DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Order.GetCurrent;

public record GetOrderCurrentQuery(Guid UserId) : IQuery<OrderDto?>;

public class GetOrderCurrentQueryHandler : IQueryHandler<GetOrderCurrentQuery, OrderDto?>
{
    private readonly QueryContext _context;

    public GetOrderCurrentQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<OrderDto?> Handle(GetOrderCurrentQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(x => x.OrderItems)
            .ThenInclude(c => c.Course.Teacher.User)
            .Include(c => c.User)
            .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.IsPay == false);

        return OrdreMapper.MapOrder(order);
    }
}
