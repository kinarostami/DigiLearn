using AutoMapper;
using BlogModules.Context;
using BlogModules.Domain;
using BlogModules.Repository.Categories;
using BlogModules.Repository.Posts;
using BlogModules.Service.DTOs.Command;
using BlogModules.Service.DTOs.Query;
using BlogModules.Services.DTOs.Query;
using BlogModules.Utils;
using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
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
    Task<OperationResult> CreateCategory(CreateBlogCategoryCommand command);
    Task<OperationResult> EditCategory(EditBlogCategoryCommand command);
    Task<OperationResult> DeleteCategory(Guid categoyId);
    Task<List<BlogCategoryDto>> GetAllCategoris();
    Task<BlogCategoryDto> GetCategoryById(Guid id);

    Task AddVisit(Guid id);
    Task<OperationResult> CreatePost(CreatePostCommand command);
    Task<OperationResult> EditPost(EditPostCommand command);
    Task<OperationResult> DeletePost(Guid id);
    Task<BlogPostDto?> GetPostById(Guid id);
    Task<BlogPostFilterItemDto?> GetPostBySlug(string slug);
    Task<BlogPostFilterResult> GetPostByFilter(BlogPostFilterParams filterParams);
}
class BlogService : IBlogService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly ILocalFileService _localFileService;
    private readonly BlogContext _context;

    public BlogService(ICategoryRepository categoryRepository, IMapper mapper, IPostRepository postRepository, BlogContext context, ILocalFileService localFileService)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _postRepository = postRepository;
        _context = context;
        _localFileService = localFileService;
    }

    public async Task AddVisit(Guid id)
    {
        var post = await _postRepository.GetAsync(id);
        if (post != null)
        {
            post.Visit += 1;
            _postRepository.Update(post);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<OperationResult> CreateCategory(CreateBlogCategoryCommand command)
    {
        var category = _mapper.Map<Category>(command);
        if (await _categoryRepository.ExistsAsync(x => x.Slug == category.Slug))
        {
            return OperationResult.Error("Slug is Exist");
        }

        _categoryRepository.Add(category);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> CreatePost(CreatePostCommand command)
    {
        var post = _mapper.Map<Post>(command);
        if (await _postRepository.ExistsAsync(f => f.Slug == command.Slug))
            return OperationResult.Error("Slug is Exist");

        if (command.ImageFile.IsImage() == false)
            return OperationResult.Error("عکس وارد شده نامعتبر است");

        if (_localFileService == null)
            throw new InvalidOperationException("_localFileService is not injected");

        var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);
        post.ImageName = imageName;
        post.Visit = 1;
        post.Description = post.Description.SanitizeText();

        _postRepository.Add(post);
        await _postRepository.Save();
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

    public async Task<OperationResult> DeletePost(Guid id)
    {
        var post = await _postRepository.GetTracking(id);
        if (post == null)
            return OperationResult.NotFound();
        if (await _postRepository.ExistsAsync(x => x.CategoryId == id))
            return OperationResult.Error("این دسته بندی قبلا استفاده شده , لطفا پست های مربوطه را حذف کنید و دوباره امتحان کنید");

        _postRepository.Delete(post);
        await _postRepository.Save();
        _localFileService.DeleteFile(BlogDirectories.PostImage,post.ImageName);
        return OperationResult.Success();
    }

    public async Task<OperationResult> EditCategory(EditBlogCategoryCommand command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);
        if (category == null)
            return OperationResult.NotFound();
        if (command.Slug != category.Slug)
        {
            if(await _categoryRepository.ExistsAsync(x => x.Slug == command.Slug))
                return OperationResult.Error("Slug is Exist");
        }

        category.Slug = command.Slug;
        category.Title = command.Title;

        _categoryRepository.Update(category);
        await _categoryRepository.Save();
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

    public async Task<BlogPostFilterResult> GetPostByFilter(BlogPostFilterParams filterParams)
    {
        var result = _context.Posts.OrderByDescending(c => c.CreationDate)
            .Include(x => x.Category).AsQueryable();

        if(string.IsNullOrWhiteSpace(filterParams.Search) == false)
            result = result.Where(x => 
            x.Title.Contains(filterParams.Search) || x.Description.Contains(filterParams.Search));

        if(string.IsNullOrWhiteSpace(filterParams.CategorySlug) == false)
            result = result.Where(x => x.Category.Slug ==  filterParams.CategorySlug);

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new BlogPostFilterResult()
        {
            Data = await result.Skip(skip).Take(filterParams.Take).Select(x => new BlogPostFilterItemDto
            {
                CreationDate = x.CreationDate,
                Title = x.Title,
                Description = x.Description,
                Slug = x.Slug,
                ImageName = x.ImageName,
                Id = x.Id,
                OwnerName = x.OwnerName,
                UserId = x.UserId,
                Visit = x.Visit,
                Category = new BlogCategoryDto()
                {
                    Id = x.CategoryId,
                    Slug = x.Category.Slug,
                    Title = x.Category.Title
                }
            }).ToListAsync()
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return model;
    }

    public async Task<BlogPostDto?> GetPostById(Guid id)
    {
        var post = _postRepository.GetAsync(id);
        if (post == null)
            return null;

        return _mapper.Map<BlogPostDto>(post);
    }

    public async Task<BlogPostFilterItemDto?> GetPostBySlug(string slug)
    {
        var post = await _context.Posts.Include(x => x.Category).FirstOrDefaultAsync(x => x.Slug == slug);
        if (post == null)
            return null;

        return new BlogPostFilterItemDto()
        {
            Slug = post.Slug,
            Title = post.Title,
            CreationDate = post.CreationDate,
            Description = post.Description,
            ImageName = post.ImageName,
            Id = post.Id,
            UserId = post.UserId,
            OwnerName = post.OwnerName,
            Visit = post.Visit,
            Category = new BlogCategoryDto()
            {
                Id = post.CategoryId,
                Slug = post.Category.Slug,
                Title = post.Category.Title
            }
        };
    }
}
