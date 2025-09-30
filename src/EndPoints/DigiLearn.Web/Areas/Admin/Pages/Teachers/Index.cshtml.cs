using CoreModue.Facade.Teacher;
using CoreModule.Application.Teacher.ToggleStatus;
using CoreModule.Query.Teacher._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Areas.Admin.Pages.Teachers
{
    [BindProperties]
    public class IndexModel : BaseRazor
    {
        private readonly ITeacherFacade _teacherFacade;

        public IndexModel(ITeacherFacade teacherFacade)
        {
            _teacherFacade = teacherFacade;
        }
        public List<TeacherDto> Teachers { get; set; }

        public async Task OnGet()
        {
            Teachers = await _teacherFacade.GetList();
        }
        public async Task<IActionResult> OnPostToggleStatus(Guid teacherId)
        {
            return await AjaxTryCatch(() =>
            {
                return _teacherFacade.ToggleStatus(new ToggleStatusCommand(teacherId));
            });
        }
    }
}
