using Common.Domain.Repository;
using CoreModule.Domain.Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Order.Repositories;

public interface IOrderRepository : IBaseRepository<Models.Order>
{
    Task<Models.Order?> GetCurrentOrderByUserId(Guid userId);
}
