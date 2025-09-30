using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using CoreModule.Application._Utils;
using CoreModule.Application.Course.Episodes.Add;
using CoreModule.Domain.Course.Models;
using CoreModule.Domain.Course.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Course.Episodes.Edit;

public class EditCourseEpisodeCommand : IBaseCommand
{
    public Guid EpisodeId { get; set; }
    public Guid CourseId { get; set; }
    public Guid SectionId { get; set; }
    public string Title { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public IFormFile? VideoFile { get; set; }
    public IFormFile? AttachmentFile { get; set; }
    public bool IsActive { get; set; }
    public bool IsFree { get; set; }
}
public class EditCourseEpisodeCommandHandler : IBaseCommandHandler<EditCourseEpisodeCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILocalFileService _localFileService;

    public EditCourseEpisodeCommandHandler(ICourseRepository courseRepository, ILocalFileService localFileService)
    {
        _courseRepository = courseRepository;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(EditCourseEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }

        var episode = course.GetEpisodeById(request.SectionId, request.EpisodeId);

        if (episode == null)
        {
            return OperationResult.NotFound();
        }

        string? attname = null;
        if (request.AttachmentFile != null)
        {
            await SaveAttachment(request.AttachmentFile, episode, course.Id);
        }
        if (request.VideoFile != null)
        {
            await SaveVideoFile(request.VideoFile, episode, course.Id);
        }
        course.EditEpisode(request.EpisodeId, request.SectionId, request.Title, request.IsActive, request.IsFree, request.TimeSpan, attname);
        await _courseRepository.Save();
        return OperationResult.Success();
    }
    private async Task<string?> SaveAttachment(IFormFile attachment, Episode episode, Guid courseId)
    {
        if (attachment.IsValidCompressFile())
        {
            var attName = episode.VideoName.Replace(".mp4", Path.GetExtension(attachment.FileName));
            await _localFileService.SaveFile(attachment, CoreModuleDirectories.CourseEpisode(courseId, episode.Token),
                attName);
            return attName;
        }


        return null;
    }
    private async Task SaveVideoFile(IFormFile videoFile, Episode episode, Guid courseId)
    {
        if (videoFile.IsValidMp4File())
        {
            await _localFileService.SaveFile(videoFile, CoreModuleDirectories.CourseEpisode(courseId, episode.Token),
                episode.VideoName);
        }
    }

}
public class EditEpisodeCommandValidator : AbstractValidator<EditCourseEpisodeCommand>
{
    public EditEpisodeCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotNull()
            .NotEmpty();
    }
}