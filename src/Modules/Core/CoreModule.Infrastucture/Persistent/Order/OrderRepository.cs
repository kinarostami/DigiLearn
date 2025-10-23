using Common.Infrastructure.Repository;
using CoreModule.Domain.Order.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Infrastucture.Persistent.Order;

public class OrderRepository : BaseRepository<Domain.Order.Models.Order, CoreMoudelEfContext>, IOrderRepository
{
    public OrderRepository(CoreMoudelEfContext context) : base(context)
    {
    }

    public async Task<Domain.Order.Models.Order?> GetCurrentOrderByUserId(Guid userId)
    {
        //return await Context.Orders.AsTracking()
        //    .FirstOrDefaultAsync(f => f.IsPay == false && f.UserId == userId);
        throw new InvalidDataException();
    }
}
