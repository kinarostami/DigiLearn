using BlogModules.Service;
using BlogModules.Service.DTOs.Query;
using BlogModules.Services.DTOs.Query;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Pages.Blog
{
    public class IndexModel : BaseRazorFilter<BlogPostFilterParams>
    {
        private readonly IBlogService _blogService;

        public IndexModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public List<BlogCategoryDto> Categories { get; set; }
        public BlogPostFilterResult FilterResult { get; set; }
        public async Task OnGet()
        {
            FilterResult = await _blogService.GetPostByFilter(FilterParams);
            Categories = await _blogService.GetAllCategoris();
        }
    }
}
