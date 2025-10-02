using BlogModules.Service;
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

    public HomePageService(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
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
                Duration = "",
                CommentCounts = 0
            }).ToList()
        };
        return model;
    }
}
