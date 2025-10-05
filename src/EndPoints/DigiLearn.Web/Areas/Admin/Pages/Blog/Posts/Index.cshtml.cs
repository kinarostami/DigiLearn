﻿using BlogModules.Service;
using BlogModules.Service.DTOs.Command;
using BlogModules.Service.DTOs.Query;
using BlogModules.Services.DTOs.Query;
using Common.Application;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using DigiLearn.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using UserModule.Core.Services;

namespace DigiLearn.Web.Areas.Admin.Pages.Blog.Posts;

public class IndexModel : BaseRazorFilter<BlogPostFilterParams>
{
    private readonly IBlogService _blogService;
    private readonly IUserFacade _userFacade;
    private readonly IRenderViewToString _renderViewToString;

    public IndexModel(IBlogService blogService, IRenderViewToString renderViewToString, IUserFacade userFacade)
    {
        _blogService = blogService;
        _renderViewToString = renderViewToString;
        _userFacade = userFacade;
    }

    public List<BlogCategoryDto> Categories { get; set; }
    public BlogPostFilterResult FilterResult { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public string Title { get; set; }

    [Display(Name = "نام نویسنده")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public string OwnerName { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [UIHint("Ckeditor4")]
    [BindProperty]
    public string Description { get; set; }

    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public string Slug { get; set; }

    [Display(Name = "عکس مقاله")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public IFormFile ImageFile { get; set; }


    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public Guid CategoryId { get; set; }

    public async Task OnGet()
    {
        Categories = await _blogService.GetAllCategoris();
        FilterResult = await _blogService.GetPostByFilter(FilterParams);

        var user = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
        if (user != null)
            OwnerName = user.Name + " " + user.Family;
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _blogService.CreatePost(new CreatePostCommand()
        {
            UserId = User.GetUserId(),
            OwnerName = OwnerName,
            CategoryId = CategoryId,
            Descriptoin = Description,
            Slug = Slug,
            Title = Title,
            ImageFile = ImageFile
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
    public async Task<IActionResult> OnPostEdit(EditPostViewModel viewModel)
    {
        return await AjaxTryCatch(async () => await _blogService.EditPost(new EditPostCommand()
        {
            OwnerName = viewModel.OwnerName,
            CategoryId = viewModel.CategoryId,
            Descriptoin = viewModel.Description,
            Slug = viewModel.Slug,
            Title = viewModel.Title,
            ImageFile = viewModel.ImageFile
        }));
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        return await AjaxTryCatch(() => _blogService.DeletePost(id));
    }

    public async Task<IActionResult> OnPostEdit(EditPostCommand command)
    {
        return await AjaxTryCatch(() => _blogService.EditPost(command));
    }

    public async Task<IActionResult> OnGetShowEditPage(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var post = await _blogService.GetPostById(id);
            if (post == null)
                return OperationResult<string>.NotFound();

            var categories = await _blogService.GetAllCategoris();

            var viewModel = new EditPostViewModel
            {
                Categories = categories,
                Id = post.Id,
                CategoryId = post.CategoryId,
                Title = post.Title,
                UserId = post.UserId,
                OwnerName = post.OwnerName,
                Description = post.Description,
                Slug = post.Slug,
            };
            var view = await _renderViewToString.RenderToStringAsync("_Edit", viewModel, PageContext);
            return OperationResult<string>.Success(view);
        });
    }
}
