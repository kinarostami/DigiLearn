using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
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

namespace CoreModule.Application.Course.Edit;

public class EditCourseCommand : IBaseCommand
{
    public Guid CourseId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string Slug { get; set; }
    public IFormFile? VideoFileName { get; set; }
    public int Price { get; set; }
    public SeoData SeoData { get; set; }
    public CourseLevel CourseLevel { get; set; }
    public CourseStatus Status { get; set; }
    public CourseActionStatus ActionStatus { get; set; }
}
public class EditCourseCommandHandler : IBaseCommandHandler<EditCourseCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseDomainService _courseDomainService;
    private readonly IFtpFileService _ftpFileService;
    private readonly ILocalFileService _localFileService;

    public EditCourseCommandHandler(ICourseRepository courseRepository, ICourseDomainService courseDomainService, IFtpFileService ftpFileService, ILocalFileService localFileService)
    {
        _courseRepository = courseRepository;
        _courseDomainService = courseDomainService;
        _ftpFileService = ftpFileService;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(EditCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();

        var imageName = course.ImageName;
        var videoPath = course.VideoName;

        var oldVideoFileName = course.VideoName;
        var oldimageFileName = course.ImageName;
        if (request.VideoFileName != null)
        {
            if (request.VideoFileName.IsValidMp4File() == false)
            {
                return OperationResult.Error("فایل ورودی باید ویدیو باشه");
            }
            videoPath = await _ftpFileService.SaveFileAndGenerateName
               (request.VideoFileName, CoreModuleDirectories.CourseDemoVideo(course.Id));
        }

        if (request.ImageFile.IsImage() == false)
        {
            imageName = await _localFileService.SaveFileAndGenerateName
               (request.ImageFile!, CoreModuleDirectories.CourseImage);
        }

        course.Edit(request.Title,request.Description,imageName,videoPath,request.Price
            ,request.SeoData,request.CourseLevel,request.Status,
            request.CategoryId,request.SubCategoryId,request.Slug,request.ActionStatus,_courseDomainService);

        await _courseRepository.Save();

        DeleteOldFiles(oldimageFileName, oldVideoFileName,
        request.VideoFileName != null,
        request.ImageFile != null, course);
        return OperationResult.Success();
    }
    void DeleteOldFiles(string image, string? video, bool isUploadNewVideo, bool isUploadNewImage, Domain.Course.Models.Course course)
    {
        if (isUploadNewVideo && string.IsNullOrWhiteSpace(video) == false)
        {
            _localFileService.DeleteFile(CoreModuleDirectories.CourseDemoVideo(course.Id), video);
        }

        if (isUploadNewImage)
        {
            _localFileService.DeleteFile(CoreModuleDirectories.CourseImage, image);
        }
    }
}
public class EditCourseCommandValidator : AbstractValidator<EditCourseCommand>
{
    public EditCourseCommandValidator()
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
