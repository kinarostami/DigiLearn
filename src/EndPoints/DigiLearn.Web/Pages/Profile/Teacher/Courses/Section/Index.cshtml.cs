using CoreModue.Facade.Course;
using CoreModue.Facade.Teacher;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Section;

[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class IndexModel : BaseRazor
{
    private readonly ICourseFacade _courseFacade;
    private readonly ITeacherFacade _teacherFacade;

    public IndexModel(ICourseFacade courseFacade, ITeacherFacade teacherFacade)
    {
        _courseFacade = courseFacade;
        _teacherFacade = teacherFacade;
    }

    public CourseDto Course { get; set; }
    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var teacher = await _teacherFacade.GetByUserId(User.GetUserId());
        var course = await _courseFacade.GetCourseById(courseId);

        if(course == null || course.TeacherId != teacher!.Id)
            return RedirectToPage("../Index");

        Course = course;

        return Page();
    }
}
