using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Order._DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Order.GetByFilter;

public class GetOrderByFilterQuery : QueryFilter<OrderFilterResult, OrderFilterParams>
{
    public GetOrderByFilterQuery(OrderFilterParams filterParams) : base(filterParams)
    {
    }
}
public class GetOrderByFilterQueryHandler : IQueryHandler<GetOrderByFilterQuery, OrderFilterResult>
{
    private readonly QueryContext _queryContext;

    public GetOrderByFilterQueryHandler(QueryContext queryContext)
    {
        _queryContext = queryContext;
    }

    public async Task<OrderFilterResult> Handle(GetOrderByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = _queryContext.Orders
            .Include(x => x.User)
            .Include(x => x.OrderItems)
            .AsQueryable();

        if (request.FilterParams.UserId != null)
        {
            result = result.Where(x => x.UserId == request.FilterParams.UserId);
        }

        if (request.FilterParams.StartDate != null)
        {
            result = result.Where(x => x.PaymentDate.Value.Date <= request.FilterParams.StartDate.Value.Date);
        }
        
        if (request.FilterParams.EndDate != null)
        {
            result = result.Where(x => x.PaymentDate.Value.Date >= request.FilterParams.EndDate.Value.Date);
        }

        switch (request.FilterParams.Status)
        {
            case OrderFilterStatus.All:
                break;
            case OrderFilterStatus.Pending:
                result = result.Where(x => x.IsPay == false);
                break;
            case OrderFilterStatus.Completed:
                result = result.Where(x => x.IsPay == true);
                break;
            default:
                break;
        }

        var skip = (request.FilterParams.PageId - 1) * request.FilterParams.Take;
        var model = new OrderFilterResult()
        {
            Data = await result.Skip(skip).Take(request.FilterParams.Take)
            .Select(x => new OrderFilterData()
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                ItemCount = x.OrderItems.Count,
                TotalPrice = x.OrderItems.Sum(x => x.Price),
                PaymentDate = x.PaymentDate,
                IsPay = x.IsPay,
                UserFullName = x.User.FullName ?? x.User.PhoneNumber,
                UserId = default
            }).ToListAsync()
        };
        model.GeneratePaging(result, request.FilterParams.Take, request.FilterParams.PageId);
        return model;
    }
}
