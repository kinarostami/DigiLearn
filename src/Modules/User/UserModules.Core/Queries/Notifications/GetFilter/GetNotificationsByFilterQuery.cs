using AutoMapper;
using Common.Query;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Core.Queries._DTOs;
using UserModules.Data;
using UserModules.Data.Entities.Notifications;

namespace UserModules.Core.Queries.Notifications.GetFilter;

public class GetNotificationsByFilterQuery : QueryFilter<NotificationFilterResult, NotificationFilterParams>
{
    public GetNotificationsByFilterQuery(NotificationFilterParams filterParams) : base(filterParams)
    {
    }
}
public class GetNotificationsByFilterQueryHandler : IQueryHandler<GetNotificationsByFilterQuery, NotificationFilterResult>
{
    private readonly UserContexts _userContext;

    public GetNotificationsByFilterQueryHandler(UserContexts userContext)
    {
        _userContext = userContext;
    }

    public async Task<NotificationFilterResult> Handle(GetNotificationsByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = _userContext.Notifications
            .Where(r => r.UserId == request.FilterParams.UserId)
            .OrderBy(d => d.IsSeen).AsQueryable();
        if (request.FilterParams.IsSeen != null)
        {
            if (request.FilterParams.IsSeen.Value == true)
            {
                result = result.Where(r => r.IsSeen);
            }
            else
            {
                result = result.Where(r => r.IsSeen == false);
            }
        }

        var skip = (request.FilterParams.PageId - 1) * request.FilterParams.Take;
        var model = new NotificationFilterResult()
        {
            Data = await result.Skip(skip).Take(request.FilterParams.Take)
                .Select(s => new NotificationFilterData
                {
                    Id = s.Id,
                    CreationDate = s.CreationDate,
                    UserId = s.UserId,
                    Text = s.Text,
                    Title = s.Title,
                    IsSeen = s.IsSeen
                }).ToListAsync()
        };
        model.GeneratePaging(result, request.FilterParams.Take, request.FilterParams.PageId);
        return model;
    }
}
