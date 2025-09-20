using AutoMapper;
using BlogModules.Domain;
using BlogModules.Repository.Categories;
using BlogModules.Repository.Posts;
using BlogModules.Service.DTOs.Command;
using BlogModules.Service.DTOs.Query;
using BlogModules.Utils;
using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlogModules.Service;

public interface IBlogService
{
    Task<OperationResult> CreateCategory(CreateCategoryCommand command);
    Task<OperationResult> EditCategory(EditCategoryCommand command);
    Task<OperationResult> DeleteCategory(Guid categoyId);
    Task<List<BlogCategoryDto>> GetAllCategoris();
    Task<BlogCategoryDto> GetCategoryById(Guid id);

    Task<OperationResult> CreatePost(CreatePostCommand command);
    Task<OperationResult> EditPost(EditPostCommand command);
    Task<OperationResult> DeletePost(Guid postId);
    Task<BlogPostDto?> GetPostById(Guid id);
}
class BlogService : IBlogService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly ILocalFileService _localFileService;

    public BlogService(ICategoryRepository categoryRepository, IMapper mapper, IPostRepository postRepository)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<OperationResult> CreateCategory(CreateCategoryCommand command)
    {
        var category = _mapper.Map<Category>(command);
        if (await _categoryRepository.ExistsAsync(x => x.Slug == category.Slug))
        {
            return OperationResult.Error("Slug is Exist");
        }

        _categoryRepository.Add(category);
        _categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> CreatePost(CreatePostCommand command)
    {
        var post = _mapper.Map<Post>(command);
        if (await _postRepository.ExistsAsync(x => x.Slug == post.Slug))
            return OperationResult.Error("Slug is Exist");

        if(command.ImageFile.IsImage() == false)
            return OperationResult.Error("عکس وارد شده نامعتبر است");

        var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);
        post.ImageName = imageName;
        post.Visit = 1;
        post.Description = post.Description.SanitizeText();

        _postRepository.Add(post);
        _postRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteCategory(Guid categoyId)
    {
        var category = await _categoryRepository.GetAsync(categoyId);
        if (category == null)
            return OperationResult.NotFound();
        if (await _postRepository.ExistsAsync(x => x.CategoryId == categoyId))
            return OperationResult.Error("این دسته بندی قبلا استفاده شده , لطفا پست های مربوطه را حذف کنید و دوباره امتحان کنید");

        _categoryRepository.Delete(category);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeletePost(Guid postId)
    {
        var post = await _postRepository.GetTracking(postId);
        if (post == null)
            return OperationResult.NotFound();
        if (await _postRepository.ExistsAsync(x => x.CategoryId == postId))
            return OperationResult.Error("این دسته بندی قبلا استفاده شده , لطفا پست های مربوطه را حذف کنید و دوباره امتحان کنید");

        _postRepository.Delete(post);
        await _postRepository.Save();
        _localFileService.DeleteFile(BlogDirectories.PostImage,post.ImageName);
        return OperationResult.Success();
    }

    public async Task<OperationResult> EditCategory(EditCategoryCommand command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);
        if (category == null)
            return OperationResult.NotFound();
        if (command.Slug != category.Slug)
        {
            if(await _categoryRepository.ExistsAsync(x => x.Slug == category.Slug))
                return OperationResult.Error("Slug is Exist");
        }

        category.Slug = command.Slug;
        category.Title = command.Title;

        _categoryRepository.Update(category);
        _categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> EditPost(EditPostCommand command)
    {
        var post = await _postRepository.GetTracking(command.Id);
        if (post == null)
            return OperationResult.NotFound();

        if (command.Slug != post.Slug)
            if (await _postRepository.ExistsAsync(x => x.Slug == post.Slug))
                return OperationResult.Error("Slug is Exist");

        if(command.ImageFile != null)
            if (command.ImageFile.IsImage() == false)
                return OperationResult.Error("عکس وارد شده نامعتبر است");
            else
            {
                var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);
                post.ImageName = imageName;
            }

        post.Description = command.Descriptoin;
        post.Slug = command.Slug;
        post.OwnerName = command.OwnerName;
        post.Title = command.Title;
        post.UserId = command.UserId;
        post.CategoryId = command.CategoryId;

        _postRepository.Update(post);
       await _postRepository.Save();
        return OperationResult.Success();
    }

    public async Task<List<BlogCategoryDto>> GetAllCategoris()
    {
        var categories = await _categoryRepository.GetAll();

        return _mapper.Map<List<BlogCategoryDto>>(categories);
    }

    public async Task<BlogCategoryDto> GetCategoryById(Guid id)
    {
        var category = _categoryRepository.GetAsync(id);

        return _mapper.Map<BlogCategoryDto>(category);
    }

    public async Task<BlogPostDto?> GetPostById(Guid id)
    {
        var post = _postRepository.GetAsync(id);
        if (post == null)
            return null;

        return _mapper.Map<BlogPostDto>(post);
    }
}
