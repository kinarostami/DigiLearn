using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Category._DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Category.GetChildren;

public record GetCourseCategoryChildrenQuery(Guid ParentId) : IQuery<List<CourseCategoryChild>>;

public class GetCourseCategoryChildrenQueryHandler : IQueryHandler<GetCourseCategoryChildrenQuery, List<CourseCategoryChild>>
{
    private readonly QueryContext _context;

    public GetCourseCategoryChildrenQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<CourseCategoryChild>> Handle(GetCourseCategoryChildrenQuery request, CancellationToken cancellationToken)
    {
        return await _context.CourseCategories
            .Where(x => x.ParentId == request.ParentId)
            .OrderByDescending(x => x.CreationDate)
            .Select(x => new CourseCategoryChild()
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Slug = x.Slug,
                CreationDate = x.CreationDate,
                Title = x.Title,
            }).ToListAsync();
    }
}
