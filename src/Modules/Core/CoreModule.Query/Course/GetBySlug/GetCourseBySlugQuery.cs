using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query._DTOs;
using CoreModule.Query.Category._DTOs;
using CoreModule.Query.Course._DTOs;
using CoreModule.Query.Teacher._DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Course.GetBySlug;

public record GetCourseBySlugQuery(string Slug) : IQuery<CourseDto?>;

public class GetCourseBySlugQueryHandler : IQueryHandler<GetCourseBySlugQuery, CourseDto>
{
    private readonly QueryContext _context;

    public GetCourseBySlugQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<CourseDto> Handle(GetCourseBySlugQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .Include(x => x.Teacher.User)
            .Include(x => x.Category)
            .Include(x => x.SubCategory)
            .Include(x => x.Sections)
            .ThenInclude(x => x.Episodes)
            .FirstOrDefaultAsync(x => x.Slug ==  request.Slug);

        if (course == null)
        {
            return null;
        }

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
            Teacher = new TeacherDto
            {
                Id = course.Teacher.Id,
                CreationDate = course.Teacher.CreationDate,
                UserName = course.Teacher.UserName,
                CvFileName = "",
                Status = course.Teacher.Status,
                User = new CoreModuleUserDto
                {
                    Id = course.Teacher.User.Id,
                    CreationDate = course.Teacher.User.CreationDate,
                    Avatar = course.Teacher.User.Avatar,
                    Name = course.Teacher.User.Name,
                    Family = course.Teacher.User.Family,
                    Email = course.Teacher.User.Email,
                    PhoneNumber = course.Teacher.User.PhoneNumber
                }
            },
            MainCategory = new CourseCategoryDto
            {
                Id = course.Category.Id,
                CreationDate = course.Category.CreationDate,
                Title = course.Category.Title,
                Slug = course.Category.Slug,
                ParentId = course.Category.ParentId,
                Children = null
            },
            SubCategory = new CourseCategoryDto
            {
                Id = course.SubCategory.Id,
                CreationDate = course.SubCategory.CreationDate,
                Title = course.SubCategory.Title,
                Slug = course.SubCategory.Slug,
                ParentId = course.SubCategory.ParentId,
                Children = null
            },
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
                    IsFree = r.IsFree
                }).ToList(),
            }).ToList()
        };
    }
}
