using Common.Application;
using CoreModule.Application.Teacher.AcceptRequest;
using CoreModule.Application.Teacher.Register;
using CoreModule.Application.Teacher.RejectRequest;
using CoreModule.Application.Teacher.ToggleStatus;
using CoreModule.Query.Teacher._DTOs;
using CoreModule.Query.Teacher.GetById;
using CoreModule.Query.Teacher.GetByUserId;
using CoreModule.Query.Teacher.GetList;
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
    Task<OperationResult> ToggleStatus(ToggleStatusCommand command);

    Task<TeacherDto?> GetById(Guid id);
    Task<TeacherDto?> GetByUserId(Guid userId);
    Task<List<TeacherDto>?> GetList();


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

    public async Task<TeacherDto?> GetById(Guid id)
    {
        return await _mediator.Send(new GetByIdTeacherQuery(id));
    }

    public async Task<TeacherDto?> GetByUserId(Guid userId)
    {
        return await _mediator.Send(new GetByUserIdTeacherQuery(userId));
    }

    public async Task<List<TeacherDto>?> GetList()
    {
        return await _mediator.Send(new GetListTeacherQuery());
    }

    public async Task<OperationResult> Register(RegisterTeacherCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RejectRequest(RejectRequestTeacherCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ToggleStatus(ToggleStatusCommand command)
    {
        return await _mediator.Send(command);
    }
}
