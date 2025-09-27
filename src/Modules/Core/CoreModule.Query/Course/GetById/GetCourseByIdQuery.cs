using Common.Query;
using CoreModule.Query.Course._DTOs;
using Microsoft.EntityFrameworkCore;
using QueryContext = CoreModule.Query._Data.QueryContext;

namespace CoreModule.Query.Course.GetById;

public record GetCourseByIdQuery(Guid Id) : IQuery<CourseDto?>;
public class GetCourseByIdHandler : IQueryHandler<GetCourseByIdQuery, CourseDto?>
{
    private readonly QueryContext _context;

    public GetCourseByIdHandler(_Data.QueryContext context)
    {
        _context = context;
    }

    public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .Include(x => x.Sections)
            .ThenInclude(x => x.Episodes)
            .FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);

        if (course == null)
            return null;

        return new CourseDto
        {
            Id = course.Id,
            CreationDate = course.CreationDate,
            TeacherId = course.TeacherId,
            CategoryId = course.CategoryId,
            SubCategoryId = course.SubCategoryId,
            Title = course.Title,
            Slug = course.Slug,
            Description = course.Description,
            ImageName = course.ImageName,
            VideoName = course.VideoName,
            Price = course.Price,
            LastUpdate = course.LastUpdate,
            SeoData = course.SeoData,
            CourseLevel = course.CourseLevel,
            CourseStatus = course.CourseStatus,
            Status = course.Status,
            Sections = course.Sections.Select(s => new CourseSectionDto()
            {
                Title = s.Title,
                Id = s.Id,
                CourseId = s.CourseId,
                CreationDate = s.CreationDate,
                DisplayOrder = s.DisplayOrder,
                Episodes = s.Episodes.Select(r => new EpisodeDto
                {
                    Id = r.Id,
                    CreationDate = r.CreationDate,
                    SectionId = r.SectionId,
                    Title = r.Title,
                    EnglishTitle = r.EnglishTitle,
                    Token = r.Token,
                    TimeSpan = r.TimeSpan,
                    VideoName = r.VideoName,
                    AttachmentName = r.AttachmentName,
                    IsActive = r.IsActive,
                    IsFree = r.IsFree,
                }).ToList()
            }).ToList()
        };
    }
}

