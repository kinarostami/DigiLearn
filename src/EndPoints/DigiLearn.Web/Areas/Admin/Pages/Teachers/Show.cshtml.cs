using CoreModue.Facade.Teacher;
using CoreModule.Query.Teacher._DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Areas.Admin.Pages.Teachers
{
    [BindProperties]
    public class ShowModel : PageModel
    {
        private readonly ITeacherFacade _teacherFacade;

        public ShowModel(ITeacherFacade teacherFacade)
        {
            _teacherFacade = teacherFacade;
        }
        public TeacherDto Teacher { get; set; }
        public async Task<IActionResult> OnGet(Guid teacherId)
        {
            var teacher = await _teacherFacade.GetById(teacherId);
            if (teacher == null)
                return RedirectToPage("Index");

            Teacher = teacher;
            return Page();
        }
    }
}
