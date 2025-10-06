using BlogModules.Service;
using BlogModules.Services.DTOs.Query;
using CommentModules.Services;
using CommentModules.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Pages.Blog
{
    public class ShowModel : PageModel
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;

        public ShowModel(IBlogService blogService, ICommentService commentService)
        {
            _blogService = blogService;
            _commentService = commentService;
        }
        public BlogPostFilterItemDto BlogPost { get; set; }
        public CommentFilterResult FilterResult { get; set; }
        public async Task<IActionResult> OnGet(string slug)
        {
            var post = await _blogService.GetPostBySlug(slug);
            if (post == null)
                return NotFound();

            FilterResult = await _commentService.GetCommentByFilter(new CommentFilterParams()
            {
                EntityId = post.Id,
                CommentType = CommentModules.Domain.CommentType.Article
            });
            
            BlogPost = post;
            _blogService.AddVisit(post.Id);
            return Page();
        }
    }
}
