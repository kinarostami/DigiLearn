using Common.Application;
using Common.Domain.Utils;
using CoreModue.Facade.Category;
using CoreModule.Application.Category.Edit;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses.Categories
{
    [BindProperties]
    public class EditModel : BaseRazor
    {
        private readonly ICourseCategoryFacade _courseCategoryFacade;

        public EditModel(ICourseCategoryFacade courseCategoryFacade)
        {
            _courseCategoryFacade = courseCategoryFacade;
        }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Slug { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var category = await _courseCategoryFacade.GetCategoryById(id);
            if (category == null)
                return RedirectToPage("Index");

            Title = category.Title;
            Slug = category.Slug;

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            var result = await _courseCategoryFacade.Edit(new EditCourseCategoryCommand()
            {
                Id = id,
                Title = Title,
                Slug = Slug.ToSlug()
            });

            return RedirectAndShowAlert(result, RedirectToPage("Index", new { id }));
        }
    }
}
