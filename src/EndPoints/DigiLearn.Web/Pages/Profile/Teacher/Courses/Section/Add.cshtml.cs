using CoreModue.Facade.Course;
using CoreModule.Application.Course.Sections.AddSection;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Section;

[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class AddModel : BaseRazor
{
    private readonly ICourseFacade _courseFacade;

    public AddModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "ترتیب نمایش")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int DisplayOrder { get; set; }

    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPost(Guid courseId)
    {
        var result = await _courseFacade.AddSection(new AddCourseSectionCommand()
        {
            CourseId = courseId,
            Title = Title,
            DisplayOrder = DisplayOrder
        });

        return RedirectAndShowAlert(result, RedirectToPage("index", new { courseId }));
    }
}
