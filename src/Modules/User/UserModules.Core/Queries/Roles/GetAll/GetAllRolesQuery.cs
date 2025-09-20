using Common.Query;
using Microsoft.EntityFrameworkCore;
using UserModules.Data;
using UserModules.Data.Entities.Roles;

namespace UserModule.Core.Queries.Roles.GetAll;

public class GetAllRolesQuery : IQuery<List<Role>>
{

}
class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, List<Role>>
{
    private readonly UserContexts _context;

    public GetAllRolesQueryHandler(UserContexts context)
    {
        _context = context;
    }

    public async Task<List<Role>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .Include(c => c.Permissions).OrderByDescending(d => d.CreationDate)
            .ToListAsync(cancellationToken);
    }
}