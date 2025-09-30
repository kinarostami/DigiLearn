using Common.Application;
using CoreModule.Domain.Teacher.Events;
using CoreModule.Domain.Teacher.Repository;
using MediatR;
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
    private readonly IMediator _mediator;

    public RejectRequestTeacherCommandHandler(ITeacherRepository teacherRepository, IMediator mediator)
    {
        _teacherRepository = teacherRepository;
        _mediator = mediator;
    }
    public async Task<OperationResult> Handle(RejectRequestTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTracking(request.TheacherId);
        if (teacher == null)
            return OperationResult.NotFound();

        _teacherRepository.Delete(teacher);
        await _teacherRepository.Save();
        await _mediator.Publish(new RejectTeacherRequestEvent()
        {
            Description = request.Descriptoin,
            UserId = teacher.UserId
        });
        return OperationResult.Success();
    }
}
