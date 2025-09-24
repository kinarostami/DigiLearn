using Common.Application;
using CoreModule.Application.Teacher.AcceptRequest;
using CoreModule.Application.Teacher.Register;
using CoreModule.Application.Teacher.RejectRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModue.Facade.Teacher;

public interface ITeacherFacade
{
    Task<OperationResult> Register(RegisterTeacherCommand command);
    Task<OperationResult> AssceptRequest(AcceptRequestTeacherCommand command);
    Task<OperationResult> RejectRequest(RejectRequestTeacherCommand command);


}
public class TeacherFacade : ITeacherFacade
{
    private readonly IMediator _mediator;

    public TeacherFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AssceptRequest(AcceptRequestTeacherCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Register(RegisterTeacherCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RejectRequest(RejectRequestTeacherCommand command)
    {
        return await _mediator.Send(command);
    }
}
