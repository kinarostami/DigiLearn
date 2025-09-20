using AutoMapper;
using BlogModules.Domain;
using BlogModules.Repository.Categories;
using BlogModules.Repository.Posts;
using BlogModules.Service.DTOs.Command;
using BlogModules.Service.DTOs.Query;
using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlogModules.Service;

public interface IBlogService
{
    Task<OperationResult> Create(CreateCategoryCommand command);
    Task<OperationResult> Edit(EditCategoryCommand command);
    Task<OperationResult> Delete(Guid categoyId);
    Task<List<BlogCategoryDto>> GetAllCategoris();
    Task<BlogCategoryDto> GetCategoryByIdTask(Guid id);
}
class BlogService : IBlogService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public BlogService(ICategoryRepository categoryRepository, IMapper mapper, IPostRepository postRepository)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<OperationResult> Create(CreateCategoryCommand command)
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

    public async Task<OperationResult> Delete(Guid categoyId)
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

    public async Task<OperationResult> Edit(EditCategoryCommand command)
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

    public async Task<List<BlogCategoryDto>> GetAllCategoris()
    {
        var categories = await _categoryRepository.GetAll();

        return _mapper.Map<List<BlogCategoryDto>>(categories);
    }

    public async Task<BlogCategoryDto> GetCategoryByIdTask(Guid id)
    {
        var category = _categoryRepository.GetAsync(id);

        return _mapper.Map<BlogCategoryDto>(category);
    }
}
