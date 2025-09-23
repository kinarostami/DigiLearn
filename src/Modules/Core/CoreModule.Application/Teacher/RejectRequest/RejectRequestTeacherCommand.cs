using Common.Application;
using CoreModule.Domain.Teacher.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Teacher.RejectRequest;

public class RejectRequestTeacherCommand : IBaseCommand
{
    public Guid TheacherId { get; set; }
    public string Descriptoin { get; set; }
}

public class RejectRequestTeacherCommandHandler : IBaseCommandHandler<RejectRequestTeacherCommand>
{
    private readonly ITeacherRepository _teacherRepository;

    public RejectRequestTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    public async Task<OperationResult> Handle(RejectRequestTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTracking(request.TheacherId);
        if (teacher == null)
            return OperationResult.NotFound();

        _teacherRepository.Delete(teacher);
        //send Event
        await _teacherRepository.Save();
        return OperationResult.Success();
    }
}
