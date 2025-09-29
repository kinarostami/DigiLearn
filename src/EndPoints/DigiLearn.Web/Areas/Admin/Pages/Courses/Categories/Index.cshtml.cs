using CoreModue.Facade.Category;
using CoreModule.Application.Category.Delete;
using CoreModule.Domain.Category.Models;
using CoreModule.Query.Category._DTOs;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Categories
{
    [BindProperties]
    public class IndexModel : BaseRazor
    {
        private readonly ICourseCategoryFacade _courseCategoryFacade;

        public IndexModel(ICourseCategoryFacade courseCategoryFacade)
        {
            _courseCategoryFacade = courseCategoryFacade;
        }

        public List<CourseCategoryDto> Categories { get; set; }
        public async Task OnGet()
        {
            Categories = await _courseCategoryFacade.GetAll();
        }
        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            return await AjaxTryCatch(() =>
            {
               return _courseCategoryFacade.Delete(new DeleteCourseCategoryCommand(id));
            });
        }
    }
}
