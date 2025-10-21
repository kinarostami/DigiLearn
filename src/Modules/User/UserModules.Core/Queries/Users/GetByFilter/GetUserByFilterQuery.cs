using Common.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Core.Queries._DTOs;
using UserModules.Data;
using UserModules.Data.Entities.Roles;

namespace UserModules.Core.Queries.Users.GetByFilter;

public class GetUserByFilterQuery : QueryFilter<UserFilterResult, UserFilterParams>
{
    public GetUserByFilterQuery(UserFilterParams filterParams) : base(filterParams)
    {
    }
}
public class GetUserByFilterQueryHandler : IQueryHandler<GetUserByFilterQuery, UserFilterResult>
{
    private readonly UserContexts _context;

    public GetUserByFilterQueryHandler(UserContexts context)
    {
        _context = context;
    }

    public async Task<UserFilterResult> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = _context.Users
            .Include(x => x.UserRoles)
            .OrderByDescending(x => x.CreationDate)
            .AsQueryable();

        if (string.IsNullOrWhiteSpace(request.FilterParams.Email) == false)
        {
            result = result.Where(x => x.Email.Contains(request.FilterParams.Email));
        }

        if (string.IsNullOrWhiteSpace(request.FilterParams.PhoneNumber) == false)
        {
            result = result.Where(x => x.PhoneNumber.Contains(request.FilterParams.PhoneNumber));
        }

        if (request.FilterParams.StartDate != null)
        {
            result = result.Where(x => x.CreationDate.Date >= request.FilterParams.StartDate.Value.Date);
        }                                            
                                                     
        if (request.FilterParams.EndDate != null)    
        {                                            
            result = result.Where(x => x.CreationDate.Date <= request.FilterParams.EndDate.Value.Date);
        }

        var skip = (request.FilterParams.PageId - 1) * request.FilterParams.Take;

        var model = new UserFilterResult()
        {
            Data = await result.Skip(skip).Take(request.FilterParams.Take).Select(x => new UserDto()
            {
                Id = x.Id,
                Avatar= x.Avatar,
                CreationDate = x.CreationDate,
                Name = x.Name,
                Family = x.Family,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Password = null,
                Roles = null
            }).ToListAsync()
        };

        model.GeneratePaging(result,request.FilterParams.Take,request.FilterParams.PageId);
        return model;
    }
}
