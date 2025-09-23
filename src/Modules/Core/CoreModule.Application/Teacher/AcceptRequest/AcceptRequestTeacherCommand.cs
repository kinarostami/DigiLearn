using Common.Application;
using CoreModule.Domain.Teacher.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Teacher.AcceptRequest;

public record AcceptRequestTeacherCommand(Guid TheacherId) : IBaseCommand;

public class AcceptRequestTeacherCommandHandler : IBaseCommandHandler<AcceptRequestTeacherCommand>
{
    private readonly ITeacherRepository _teacherRepository;

    public AcceptRequestTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<OperationResult> Handle(AcceptRequestTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTracking(request.TheacherId);
        if (teacher == null)
            return OperationResult.NotFound();

        teacher.AcceptRequest();

        await _teacherRepository.Save();
        return OperationResult.Success();
    }
}

