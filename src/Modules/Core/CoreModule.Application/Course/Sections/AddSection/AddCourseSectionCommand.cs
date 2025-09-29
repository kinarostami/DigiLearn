using Common.Application;
using CoreModule.Domain.Course.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Course.Sections.AddSection;

public class AddCourseSectionCommand : IBaseCommand
{
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public int DisplayOrder { get; set; }
}
public class AddCourseSectionCommandHandler : IBaseCommandHandler<AddCourseSectionCommand>
{
    private readonly ICourseRepository _courseRepository;

    public AddCourseSectionCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<OperationResult> Handle(AddCourseSectionCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();
        
        course.AddSection(request.Title, request.DisplayOrder);
        await _courseRepository.Save();
        return OperationResult.Success();
    }
}
public class AddCourseSectionCommandValidator : AbstractValidator<AddCourseSectionCommand>
{
    public AddCourseSectionCommandValidator()
    {
        RuleFor(x => x.Title) 
           .NotEmpty()
           .NotNull();
    }
}
