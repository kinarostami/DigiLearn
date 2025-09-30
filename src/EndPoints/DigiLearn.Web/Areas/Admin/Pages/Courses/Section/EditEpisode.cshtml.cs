using Common.Application.Validation.CustomValidation.IFormFile;
using CoreModue.Facade.Course;
using CoreModule.Application.Course.Episodes.Edit;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Section;

[BindProperties]
public class EditEpisodeModel : BaseRazor
{
    private readonly ICourseFacade _courseFacade;

    public EditEpisodeModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public string Title { get; set; }

    [Display(Name = "مدت زمان")]
    [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage = "لطفا زمان را با فرمت درست وارد کنید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public TimeSpan TimeSpan { get; set; }

    [Display(Name = "ویدیو")]
    [FileType("mp4", ErrorMessage = "ویدیو نامعتبر است")]
    [BindProperty]
    public IFormFile VideoFile { get; set; }

    [Display(Name = "فایل ضمینه")]
    [FileType("rar", ErrorMessage = "ویدیو نامعتبر است")]
    [BindProperty]
    public IFormFile? AttachmentFile { get; set; }
    
    [BindProperty]
    public bool IsActive { get; set; }
    public string? VideoFileName { get; set; }
    public bool IsFree { get; set; }

    public Guid? CourseId { get; set; }
    public EpisodeDto? Episode { get; set; } = new EpisodeDto();

    public async Task<IActionResult> OnGet(Guid episodeId,Guid courseId)
    {
        var episode = await _courseFacade.GetCourseEpisodeById(episodeId);
        if (episode == null)
            return RedirectToPage("../Index",new {courseId});

        Title = episode.Title;
        TimeSpan = episode.TimeSpan;
        IsActive = episode.IsActive;
        IsFree = episode.IsFree;
        VideoFileName = episode.VideoName;
        Episode = episode;
        CourseId = courseId;
        return Page();
    }

    public async Task<IActionResult> OnPost(Guid episodeId,Guid courseId)
    {
        var episode = await _courseFacade.GetCourseEpisodeById(episodeId);
        var result = await _courseFacade.EditEpisode(new EditCourseEpisodeCommand()
        {
            Title = Title,
            TimeSpan = TimeSpan,
            VideoFile = VideoFile,
            AttachmentFile = AttachmentFile,
            CourseId = courseId,
            SectionId = episode.SectionId,
            IsActive = IsActive,
            IsFree = IsFree,
            EpisodeId = episodeId
        });

        return RedirectAndShowAlert(result, RedirectToPage("index", new { courseId }));
    }
}
