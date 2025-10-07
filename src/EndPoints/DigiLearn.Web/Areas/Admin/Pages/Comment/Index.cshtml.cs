using CommentModules.Services;
using CommentModules.Services.DTOs;
using Common.Application.DateUtil;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DigiLearn.Web.Pages.Comment
{
    public class IndexModel : BaseRazorFilter<CommentFilterParams>
    {
        private readonly ICommentService _commentService;

        public IndexModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public AllCommentFilterReulst FilterReulst { get; set; }

        public async Task OnGet(string stDate,string enDate)
        {
            if (string.IsNullOrWhiteSpace(stDate) == false)
                FilterParams.StartDate = stDate.ToMiladi();

            if (string.IsNullOrWhiteSpace(enDate) == false)
                FilterParams.EndDate = enDate.ToMiladi();

            FilterReulst = await _commentService.GetAllComments(FilterParams);
        }
        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            return await AjaxTryCatch(() => _commentService.DeleteComment(id));
        }
    }
}
