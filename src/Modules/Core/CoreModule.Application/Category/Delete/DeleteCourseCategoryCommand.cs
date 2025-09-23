using Common.Application;
using CoreModule.Domain.Category.DomainServices;
using CoreModule.Domain.Category.Models;
using CoreModule.Domain.Category.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Category.Delete;

public record DeleteCourseCategoryCommand(Guid CategoryId) : IBaseCommand;

public class DeleteCourseCategoryCommandHandler : IBaseCommandHandler<DeleteCourseCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCourseCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<OperationResult> Handle(DeleteCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetTracking(request.CategoryId);
        if (category == null)
            return OperationResult.NotFound();

        await _categoryRepository.Delete(category);
        return OperationResult.Success();
    }
}
