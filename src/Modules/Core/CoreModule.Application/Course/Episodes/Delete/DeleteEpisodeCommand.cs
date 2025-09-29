using Common.Application;
using Common.Application.FileUtil.Interfaces;
using CoreModule.Application._Utils;
using CoreModule.Domain.Course.Models;
using CoreModule.Domain.Course.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Course.Episodes.Delete;

public record DeleteEpisodeCommand(Guid CourseId,Guid EpisodeId) : IBaseCommand;
public class DeleteEpisodeCommandHandler : IBaseCommandHandler<DeleteEpisodeCommand>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILocalFileService _localFileService;

    public DeleteEpisodeCommandHandler(ICourseRepository courseRepository, ILocalFileService localFileService)
    {
        _courseRepository = courseRepository;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(DeleteEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();

        var episode = course.DeleteEpisode(request.EpisodeId);
        await _courseRepository.Save();
        _localFileService.DeleteDirectory(CoreModuleDirectories.CourseEpisode(course.Id, episode.Token));
        return OperationResult.Success();
    }
}

