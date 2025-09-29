using Common.Application;
using CoreModule.Application.Category.AddChild;
using CoreModule.Application.Category.Create;
using CoreModule.Application.Category.Delete;
using CoreModule.Application.Category.Edit;
using CoreModule.Query.Category._DTOs;
using CoreModule.Query.Category.GetAll;
using CoreModule.Query.Category.GetById;
using CoreModule.Query.Category.GetChildren;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModue.Facade.Category;

public interface ICourseCategoryFacade
{
    Task<OperationResult> Create(CreateCourseCategoryCommand command);
    Task<OperationResult> Edit(EditCourseCategoryCommand command);
    Task<OperationResult> Delete(DeleteCourseCategoryCommand command);
    Task<OperationResult> AddChild(AddChildCategoryCommand command);

    Task<List<CourseCategoryDto>> GetAll();
    Task<List<CourseCategoryChild>> GetCategoryChild(Guid parentId);
    Task<CourseCategoryDto?> GetCategoryById(Guid id);
}
public class CourseCategoryFacade : ICourseCategoryFacade
{
    private readonly IMediator _mediator;

    public CourseCategoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddChild(AddChildCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateCourseCategoryCommand command)
    {
        return await  _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteCourseCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<List<CourseCategoryDto>> GetAll()
    {
        return await _mediator.Send(new GetAllCourseCategoryQuery());
    }

    public async Task<CourseCategoryDto?> GetCategoryById(Guid id)
    {
        return await _mediator.Send(new GetCourseCategoryById(id));
    }

    public async Task<List<CourseCategoryChild>> GetCategoryChild(Guid parentId)
    {
        return await _mediator.Send(new GetCourseCategoryChildrenQuery(parentId));
    }
}
