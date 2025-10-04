using CoreModue.Facade.Course;
using CoreModule.Application._Utils;
using CoreModule.Query.Course._DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages
{
    [BindProperties]
    public class CourseModel : PageModel
    {
        private readonly ICourseFacade _courseFacade;

        public CourseModel(ICourseFacade courseFacade)
        {
            _courseFacade = courseFacade;
        }

        public CourseDto Course { get; set; }
        public async Task<IActionResult> OnGet(string slug)
        {
            var course = await _courseFacade.GetCourseBySlug(slug);
            if (course == null)
                return NotFound();

            Course = course;
            return Page();
        }
        public async Task<IActionResult> OnGetShowOnline(string slug,Guid sectionId,Guid token)
        {
            var course = await _courseFacade.GetCourseBySlug(slug);
            if (course == null)
                return NotFound();

            var section = course.Sections.First(x => x.Id == sectionId);
            var episode = section.Episodes.FirstOrDefault(x => x.Token == token);
            if (episode == null)
                return NotFound();

            return Content(CoreModuleDirectories.GetEpisodeFile(course.Id,token,episode.VideoName));
        }
        public async Task<IActionResult> OnGetDownload(string slug,Guid sectionId,Guid token)
        {
            var course = await _courseFacade.GetCourseBySlug(slug);
            if (course == null)
                return NotFound();

            var section = course.Sections.First(x => x.Id == sectionId);
            var episode = section.Episodes.FirstOrDefault(x => x.Token == token);
            if (episode == null)
                return NotFound();

            var fileName = Path.Combine(Directory.GetCurrentDirectory() +
                CoreModuleDirectories.GetEpisodeFile(course.Id, token, episode.VideoName));

            var file = new FileStream(fileName, FileMode.Open);

            return File(file,"application/force-download",episode.VideoName);
        }
    }
}
