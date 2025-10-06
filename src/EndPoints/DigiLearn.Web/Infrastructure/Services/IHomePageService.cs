using BlogModules.Service;
using BlogModules.Services.DTOs.Query;
using CoreModue.Facade.Course;
using CoreModule.Domain.Course.Enums;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.ViewModels;

namespace DigiLearn.Web.Infrastructure.Services;

public interface IHomePageService
{
    Task<HomePageViewModel> GetData();
}
public class HomePageService : IHomePageService
{
    private readonly ICourseFacade _courseFacade;
    private readonly IBlogService _blogService;

    public HomePageService(ICourseFacade courseFacade, IBlogService blogService)
    {
        _courseFacade = courseFacade;
        _blogService = blogService;
    }

    public async Task<HomePageViewModel> GetData()
    {
        var courses = await _courseFacade.GetCourseFilter(new CourseFilterParams()
        {
            Take = 8,
            ActionStatus = CourseActionStatus.Active,
            PageId = 1,
            FilterSort = CourseFilterSort.Latest
        });

        var post = await _blogService.GetPostByFilter(new BlogPostFilterParams()
        {
            Take = 6,
            PageId = 1,
        });

        var model = new HomePageViewModel()
        {
            LatestCourses = courses.Data.Select(x => new CourseCardViewModel()
            {
                Title = x.Title,
                Slug = x.Slug,
                ImageName = x.ImageName,
                TeacherName = x.Teacher,
                Price = x.Price,
                visit = 0,
                Duration = x.GetDuration(),
                CommentCounts = 0
            }).ToList(),
            LatestArticles = post.Data
        };
        return model;
    }
}
