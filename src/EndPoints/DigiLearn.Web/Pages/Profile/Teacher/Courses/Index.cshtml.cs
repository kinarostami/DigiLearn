using CoreModue.Facade.Course;
using CoreModue.Facade.Teacher;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses;

[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class IndexModel : BaseRazorFilter<CourseFilterParams>
{
    private readonly ITeacherFacade _teacherFacade;
    private readonly ICourseFacade _courseFacade;

    public IndexModel(ITeacherFacade teacherFacade, ICourseFacade courseFacade)
    {
        _teacherFacade = teacherFacade;
        _courseFacade = courseFacade;
    }
    public CourseFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        var teacher = await _teacherFacade.GetByUserId(User.GetUserId());
        FilterParams.TeacherId = teacher?.Id;
        FilterResult = await _courseFacade.GetCourseFilter(FilterParams);
    }
}
