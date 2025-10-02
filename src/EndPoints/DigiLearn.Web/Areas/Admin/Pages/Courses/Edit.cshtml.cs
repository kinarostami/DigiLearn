using Common.Application;
using Common.Application.Validation.CustomValidation.IFormFile;
using Common.Domain.ValueObjects;
using CoreModue.Facade.Course;
using CoreModule.Application.Course.Edit;
using CoreModule.Domain.Course.Enums;
using DigiLearn.Web.Infrastructure.RazorUtils;
using DigiLearn.Web.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses;

[BindProperties]
public class EditModel : BaseRazor
{
    private readonly ICourseFacade _courseFacade;

    public EditModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }
    public Guid CourseId { get; set; }

    [Display(Name = "دسته بندی اصلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public Guid CategoryId { get; set; }

    [Display(Name = "زیر دسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public Guid SubCategoryId { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [UIHint("Ckeditor4")]
    public string Description { get; set; }

    [Display(Name = "عکس")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "عنوان انگلیسی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }

    [Display(Name = "ویدیو معرفی دوره")]
    [FileType("mp4", ErrorMessage = "ویدیو نامعتبر است")]
    public IFormFile? VideoFileName { get; set; }

    [Display(Name = "قیمت(رایگان=0)")]
    public int Price { get; set; }
    public SeoDataViewModel SeoData { get; set; }

    [Display(Name = "سطح دوره")]
    public CourseLevel CourseLevel { get; set; }

    [Display(Name = "وضعیت دوره")]
    public CourseStatus Status { get; set; }

    [Display(Name = "وضعیت")]
    public CourseActionStatus ActionStatus { get; set; }

    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var course = await _courseFacade.GetCourseById(courseId);
        if (course == null)
            return RedirectToPage("index");

        Title = course.Title;
        CategoryId = course.CategoryId;
        Slug = course.Slug;
        Description = course.Description;
        SeoData = SeoDataViewModel.ConvertToViewModel(course.SeoData);
        CourseLevel = course.CourseLevel;
        Status = course.CourseStatus;
        ActionStatus = course.Status;
        return Page();
    }
    public async Task<IActionResult> OnPost(Guid courseId)
    {
        var result = await _courseFacade.Edit(new EditCourseCommand()
        {
            CourseId = courseId,
            Title = Title,
            ImageFile = ImageFile,
            Description = Description,
            Price = Price,
            VideoFileName = VideoFileName,
            SubCategoryId = SubCategoryId,
            CategoryId = CategoryId,
            Slug = Slug,
            Status = Status,
            SeoData = SeoData.Map(),
            CourseLevel=CourseLevel ,
            ActionStatus =ActionStatus
        });
        return RedirectAndShowAlert(result, RedirectToPage("index"));
    }
}
