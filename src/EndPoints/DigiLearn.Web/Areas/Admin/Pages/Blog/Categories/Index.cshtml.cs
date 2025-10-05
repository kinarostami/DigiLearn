using BlogModules.Service;
using BlogModules.Service.DTOs.Command;
using BlogModules.Service.DTOs.Query;
using Common.Application;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DigiLearn.Web.Pages.Blog.Categories;

public class IndexModel : BaseRazor
{
    private readonly IBlogService _blogService;
    private readonly IRenderViewToString _renderViewToString;

    public IndexModel(IBlogService blogService, IRenderViewToString renderViewToString)
    {
        _blogService = blogService;
        _renderViewToString = renderViewToString;
    }
    [BindProperty]
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }
    
    [BindProperty]
    [Display(Name = "Slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }
    public List<BlogCategoryDto> Categories { get; set; }
    public async Task OnGet()
    {
        Categories = await _blogService.GetAllCategoris();
    }

    public async Task<IActionResult> OnGetShowEditPage(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var category = await _blogService.GetCategoryById(id);
            if (category == null)
            {
                return OperationResult<string>.NotFound();
            }

            var viewResult = await _renderViewToString.RenderToStringAsync("_Edit", new EditBlogCategoryCommand()
            {
                Title = category.Title,
                Slug = category.Slug,
                Id = id
            }, PageContext);

            return OperationResult<string>.Success(viewResult);
        });
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        return await AjaxTryCatch(() => _blogService.DeleteCategory(id));
    }

    public async Task<IActionResult> OnPostEdit(EditBlogCategoryCommand command)
    {
        return await AjaxTryCatch(() => _blogService.EditCategory(command));
    }
    public async Task<IActionResult> OnPost()
    {
        return await AjaxTryCatch(() => _blogService.CreateCategory(new CreateBlogCategoryCommand()
        {
            Title = Title,
            Slug = Slug,
        }));
    }
}
