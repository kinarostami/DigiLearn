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
        var model = await _context.CourseCategories
            .Include(x => x.Childs)
            .Where(x => x.ParentId == null)
            .OrderByDescending(x => x.CreationDate)
            .Select(x => new CourseCategoryDto()
            {
               Id = x.Id,
               ParentId = x.ParentId,
               Slug = x.Slug,
               CreationDate = x.CreationDate,
               Title = x.Title,
               Children = x.Childs.Select(c => new CourseCategoryChild()
               {
                   CreationDate = c.CreationDate,
                   Id = c.Id,
                   ParentId = c.ParentId,
                   Title = c.Title,
                   Slug = c.Slug,
               }).ToList()
            }).ToListAsync(cancellationToken);

        return model;
    }
}
