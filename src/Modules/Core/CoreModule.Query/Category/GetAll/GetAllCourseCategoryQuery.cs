using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Category._DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Category.GetAll;

public class GetAllCourseCategoryQuery : IQuery<List<CourseCategoryDto>>
{
}
public class GetAllCourseCategoryQueryHandler : IQueryHandler<GetAllCourseCategoryQuery, List<CourseCategoryDto>>
{
    private readonly QueryContext _context;

    public GetAllCourseCategoryQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<CourseCategoryDto>> Handle(GetAllCourseCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _context.CourseCategories
            .Where(x => x.ParentId == null)
            .OrderByDescending(x => x.CreationDate)
            .Select(x => new CourseCategoryDto()
            {
               Id = x.Id,
               ParentId = x.ParentId,
               Slug = x.Slug,
               CreationDate = x.CreationDate,
               Title = x.Title,
            }).ToListAsync();
    }
}
