using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.Utils;
using CoreModule.Application._Utils;
using CoreModule.Domain.Course.Models;
using CoreModule.Domain.Course.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Course.Episodes.Add;

public class AddCourseEpisode : IBaseCommand
{
    public Guid CourseId { get; set; }
    public Guid SectionId { get; set; }
    public string Title { get;   set; }
    public string EnglishTitle { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public IFormFile VideoFile { get; set; }
    public IFormFile? AttachmentFile { get; set; }
    public bool IsActive { get; set; }
    public bool IsFree { get; set; }
}
public class AddCourseEpisodeHandler : IBaseCommandHandler<AddCourseEpisode>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILocalFileService _localFileService;
    private readonly IFtpFileService _fpFileService;

    public AddCourseEpisodeHandler(ICourseRepository courseRepository, IFtpFileService fpFileService, ILocalFileService localFileService)
    {
        _courseRepository = courseRepository;
        _fpFileService = fpFileService;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(AddCourseEpisode request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();

        string attExName = null;
        if (request.AttachmentFile != null && request.AttachmentFile.IsValidCompressFile())
        {
            attExName = Path.GetExtension(request.AttachmentFile.FileName);
        }
        var episode = course.AddEpisode(request.SectionId, request.Title,Guid.NewGuid(), request.TimeSpan,
            Path.GetExtension(request.VideoFile.FileName),attExName , request.IsActive,
            request.EnglishTitle.ToSlug(), request.IsFree);

       await SaveFiles(request,episode);
       
       
        await _courseRepository.Save();
        return OperationResult.Success();
    }
    private async Task SaveFiles(AddCourseEpisode request, Episode episode)
    {
        await _localFileService.SaveFile(request.VideoFile,
            CoreModuleDirectories.CourseEpisode(request.CourseId, episode.Token), episode.VideoName);
        if(request.AttachmentFile != null)
        {
            if (request.AttachmentFile.IsValidCompressFile())
            {
                await _localFileService.SaveFile(request.VideoFile,
            CoreModuleDirectories.CourseEpisode(request.CourseId, episode.Token), episode.AttachmentName!);
            }
        }
    }
}
public class AddCourseEpisodeValidator : AbstractValidator<AddCourseEpisode>
{
    public AddCourseEpisodeValidator()
    {
        
    }
}
