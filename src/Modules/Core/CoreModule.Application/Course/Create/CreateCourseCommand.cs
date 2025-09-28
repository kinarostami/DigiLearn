using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.ValueObjects;
using CoreModule.Application._Utils;
using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Course.Enums;
using CoreModule.Domain.Course.Models;
using CoreModule.Domain.Course.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Course.Create;

public class CreateCourseCommand : IBaseCommand
{
    public Guid TeacherId { get;  set; }
    public Guid CategoryId { get;  set; }
    public Guid SubCategoryId { get;  set; }
    public string Title { get;  set; }
    public string Description { get; set; }
    public IFormFile ImageFile { get; set; }    
    public string Slug { get; set; }
    public IFormFile? VideoFileName { get; set; }
    public int Price { get; set; }
    public SeoData SeoData { get; set; }
    public CourseLevel CourseLevel { get; set; }
    public CourseActionStatus Status { get; set; }
}
public class CreateCourseCommandHandler : IBaseCommandHandler<CreateCourseCommand>
{
    private readonly IFtpFileService _fileService;
    private readonly ILocalFileService _localFileService;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseDomainService _courseDomainService;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, ICourseDomainService courseDomainService, IFtpFileService fileService, ILocalFileService localFileService)
    {
        _courseRepository = courseRepository;
        _courseDomainService = courseDomainService;
        _fileService = fileService;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _localFileService.SaveFileAndGenerateName(request.ImageFile,CoreModuleDirectories.CourseImage);

        string videoPath = null;
        Guid courseId = Guid.NewGuid();
        if (request.VideoFileName != null)
        {
            if (request.VideoFileName.IsValidMp4File() == false)
            {
                return OperationResult.Error("فایل ورودی باید ویدیو باشه");
            }
             //videoPath = await _fileService.SaveFileAndGenerateName
             //   (request.VideoFileName,CoreModuleDirectories.CourseDemoVideo(courseId));
        }

        var course = new Domain.Course.Models.Course(request.TeacherId, request.Title, request.Description,
            imageName, videoPath, request.Price, request.SeoData, request.CourseLevel,
            request.CategoryId, request.SubCategoryId, request.Slug, _courseDomainService,request.Status)
        {
            Id = courseId
        };

        _courseRepository.Add(course);
        await _courseRepository.Save();
        return OperationResult.Success();
    }
}

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Slug)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .NotNull();
    }
}
