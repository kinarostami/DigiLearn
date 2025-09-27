using CoreModue.Facade.Category;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DigiLearn.Web.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ICourseCategoryFacade _courseCategoryFacade;

        public AjaxController(ICourseCategoryFacade courseCategoryFacade)
        {
            _courseCategoryFacade = courseCategoryFacade;
        }

        [Route("/ajax/getCategoryChildren")]
        public async Task<IActionResult> GetCategoryChildren(Guid id)
        {
            var text = "";
            var children = await _courseCategoryFacade.GetCategoryChild(id);
            foreach (var item in children)
            {
                text += $"<option value='{item.Id}'>{item.Title}</option>";
            }

            return new ObjectResult(text);
        }
    }
}
