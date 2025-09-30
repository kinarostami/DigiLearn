using CoreModue.Facade.Teacher;
using CoreModule.Application.Teacher.AcceptRequest;
using CoreModule.Application.Teacher.RejectRequest;
using CoreModule.Query.Teacher._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Areas.Admin.Pages.Teachers
{
    [BindProperties]
    public class ShowModel : BaseRazor
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
        public async Task<IActionResult> OnPostAccept(Guid teacherId)
        {
            return await AjaxTryCatch(() =>
            {
               return _teacherFacade.AssceptRequest(new AcceptRequestTeacherCommand(teacherId));
            });
        }
        public async Task<IActionResult> OnPostReject(Guid teacherId, string description)
        {
            var result = await _teacherFacade.RejectRequest(new RejectRequestTeacherCommand()
            {
                TheacherId = teacherId,
                Descriptoin = description
            });
            return RedirectAndShowAlert(result, RedirectToPage("Show", new { teacherId }));
        }
    }
}
