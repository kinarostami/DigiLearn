using Common.Application;
using CoreModule.Domain.Teacher.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Teacher.ToggleStatus;

public record ToggleStatusCommand(Guid TeacherId) : IBaseCommand
{
}
public record ToggleStatusCommandHandler : IBaseCommandHandler<ToggleStatusCommand>
{
    private readonly ITeacherRepository _teacherRepository;

    public ToggleStatusCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<OperationResult> Handle(ToggleStatusCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTracking(request.TeacherId);
        if (teacher == null)
            return OperationResult.NotFound();

        teacher.ToggleStatus();
        await _teacherRepository.Save();
        return OperationResult.Success();
    }
}
