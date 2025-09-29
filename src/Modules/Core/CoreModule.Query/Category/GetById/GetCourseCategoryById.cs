using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Category._DTOs;
using CoreModule.Query.Course._DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Category.GetById;

public record GetCourseCategoryById(Guid Id) : IQuery<CourseCategoryDto?>;

public class GetCourseCategoryByIdHandler : IQueryHandler<GetCourseCategoryById, CourseCategoryDto>
{
    private readonly QueryContext _context;

    public GetCourseCategoryByIdHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<CourseCategoryDto> Handle(GetCourseCategoryById request, CancellationToken cancellationToken)
    {
        var category = await _context
            .CourseCategories
            .Include(x => x.Childs)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (category == null)
            return null;

        return new CourseCategoryDto()
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            Title = category.Title,
            Slug = category.Slug,
            ParentId = category.ParentId,
            Children = category.Childs.Select(x => new CourseCategoryChild()
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Title = x.Title,
                Slug = x.Slug,
                CreationDate = x.CreationDate,
            }).ToList()
        };
    }
}
