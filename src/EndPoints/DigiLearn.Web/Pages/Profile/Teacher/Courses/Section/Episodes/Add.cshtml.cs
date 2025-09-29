using Common.Application.Validation.CustomValidation.IFormFile;
using CoreModue.Facade.Course;
using CoreModule.Application.Course.Episodes.Add;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Section.Episodes;

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

    [Display(Name = "عنوان انگلیسی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string EnglishTitle { get; set; }

    [Display(Name = "مدت زمان")]
    [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage = "لطفا زمان را با فرمت درست وارد کنید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public TimeSpan TimeSpan { get; set; }

    [Display(Name = "ویدیو")]
    [FileType("mp4",ErrorMessage = "ویدیو نامعتبر است")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public IFormFile VideoFile { get; set; }

    [Display(Name = "فایل ضمینه")]
    [FileType("rar", ErrorMessage = "ویدیو نامعتبر است")]
    public IFormFile? AttachmentFile { get; set; }


    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPost(Guid courseId, Guid sectionId)
    {
        var result = await _courseFacade.AddEpisodes(new AddCourseEpisode()
        {
            CourseId = courseId,
            SectionId = sectionId,
            Title = Title,
            EnglishTitle = EnglishTitle,
            TimeSpan = TimeSpan,
            VideoFile = VideoFile,
            AttachmentFile = AttachmentFile,
            IsActive = false,
            IsFree = false
        });

        return RedirectAndShowAlert(result, RedirectToPage("../Index", new { courseId }));
    }
}
