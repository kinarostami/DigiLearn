using Common.Application;
using MediatR;
using UserModule.Core.Commands.Users.FullEdit;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Queries.Users.GetById;
using UserModule.Core.Queries.Users.GetByPhoneNumber;
using UserModules.Core.Commands.Users.ChangeAvatar;
using UserModules.Core.Commands.Users.ChangePassword;
using UserModules.Core.Commands.Users.Edit;
using UserModules.Core.Commands.Users.Register;
using UserModules.Core.Queries.Users.GetByFilter;

namespace UserModule.Core.Services;

public interface IUserFacade
{
    Task<OperationResult<Guid>> RegisterUser(RegisterUserCommand command);
    Task<OperationResult> EditUserProfile(EditUserCommand command);
    Task<OperationResult> EditUser(FullEditUserCommand command);
    Task<OperationResult> ChangeAvatar(ChangeUserAvatarCommand command);
    Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);
    
    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
    Task<UserDto?> GetById(Guid id);
    Task<UserFilterResult> GetByFilter(UserFilterParams filterParams);
}


public class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;

    public UserFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<Guid>> RegisterUser(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditUserProfile(EditUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditUser(FullEditUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ChangeAvatar(ChangeUserAvatarCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<UserDto?> GetById(Guid id)
    {
        return await _mediator.Send(new GetUserByIdQuery(id));

    }

    public async Task<UserFilterResult> GetByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams));
    }
}