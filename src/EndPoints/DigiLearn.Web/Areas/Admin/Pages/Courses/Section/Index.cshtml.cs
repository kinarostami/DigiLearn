using CoreModue.Facade.Course;
using CoreModue.Facade.Teacher;
using CoreModule.Application.Course.Episodes.Accept;
using CoreModule.Application.Course.Episodes.Delete;
using CoreModule.Query.Course._DTOs;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Section
{
    public class IndexModel : BaseRazor
    {
        private readonly ICourseFacade _courseFacade;

        public IndexModel(ICourseFacade courseFacade)
        {
            _courseFacade = courseFacade;
        }

        public CourseDto Course { get; set; }
        public async Task<IActionResult> OnGet(Guid courseId)
        {
            var course = await _courseFacade.GetCourseById(courseId);

            if (course == null)
                return RedirectToPage("../Index");

            Course = course;

            return Page();
        }
        public async Task<IActionResult> OnPostAccept(Guid courseId,Guid episodeId)
        {
            return await AjaxTryCatch(() =>
            {
                return _courseFacade.AcceptEpisode(new AcceptEpisodeCommand(courseId, episodeId));
            });
        }
        public async Task<IActionResult> OnPostDelete(Guid courseId,Guid episodeId)
        {
            return await AjaxTryCatch(() =>
            {
                return _courseFacade.DeleteEpisode(new DeleteEpisodeCommand(courseId, episodeId));
            });
        }

    }
}
