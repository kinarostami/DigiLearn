using Common.Application;
using Common.Application.FileUtil.Interfaces;
using CoreModule.Application._Utils;
using CoreModule.Domain.Teacher.DomainServices;
using CoreModule.Domain.Teacher.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Teacher.Register;

public class RegisterTeacherCommand : IBaseCommand
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public IFormFile CvFile { get; set; }
}
public class RegisterTeacherCommandHandler : IBaseCommandHandler<RegisterTeacherCommand>
{
    private readonly ILocalFileService _localFileService;
    private readonly ITeacherRepository _teacherRepository;
    private readonly ITeacherDomainService _teacherDomainService;

    public RegisterTeacherCommandHandler(ITeacherRepository teacherRepository, ITeacherDomainService teacherDomainService, ILocalFileService localFileService)
    {
        _teacherRepository = teacherRepository;
        _teacherDomainService = teacherDomainService;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
    {
        var cvFileName = await _localFileService.SaveFileAndGenerateName(request.CvFile, CoreModuleDirectories.CvFileName);
        var teacher = new Domain.Teacher.Models.Teacher(request.UserId,request.UserName,cvFileName, _teacherDomainService);

        _teacherRepository.Add(teacher);
        await _teacherRepository.Save();
        return OperationResult.Success();
    }
}
public class RegisterTeacherCommandValidator : AbstractValidator<RegisterTeacherCommand>
{
    public RegisterTeacherCommandValidator()
    {
        RuleFor(x => x.CvFile)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull();
    }
}
