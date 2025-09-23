using Common.Application;
using CoreModule.Domain.Category.DomainServices;
using CoreModule.Domain.Category.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Category.Edit;

public class EditCourseCategoryCommand : IBaseCommand
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
}
public class EditCourseCategoryCommandHandler : IBaseCommandHandler<EditCourseCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    public EditCourseCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }
    public async Task<OperationResult> Handle(EditCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetTracking(request.Id);
        if (category == null)
            return OperationResult.NotFound();

        category.Edit(request.Title, request.Slug, _categoryDomainService);

        await _categoryRepository.Save();
        return OperationResult.Success();
    }
}
public class EditCourseCategoryCommandValidator : AbstractValidator<EditCourseCategoryCommand>
{
    public EditCourseCategoryCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Slug)
            .NotEmpty()
            .NotNull();
    }
}
