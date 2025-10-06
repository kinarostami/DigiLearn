using BlogModules.Service;
using BlogModules.Services.DTOs.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Pages.Blog
{
    public class ShowModel : PageModel
    {
        private readonly IBlogService _blogService;

        public ShowModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public BlogPostFilterItemDto BlogPost { get; set; }
        public async Task<IActionResult> OnGet(string slug)
        {
            var post = await _blogService.GetPostBySlug(slug);
            if (post == null)
                return NotFound();
            
            BlogPost = post;
            _blogService.AddVisit(post.Id);
            return Page();
        }
    }
}
