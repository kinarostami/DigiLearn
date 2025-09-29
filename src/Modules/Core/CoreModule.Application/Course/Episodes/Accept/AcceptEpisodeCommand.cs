using Common.Application;
using CoreModule.Domain.Course.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Application.Course.Episodes.Accept;

public record AcceptEpisodeCommand(Guid CourseId,Guid EpisodeId) : IBaseCommand;

public class AcceptEpisodeCommandHandler : IBaseCommandHandler<AcceptEpisodeCommand>
{
    private readonly ICourseRepository _courseRepository;

    public AcceptEpisodeCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<OperationResult> Handle(AcceptEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();

        course.AcceptEpisode(request.EpisodeId);
        await _courseRepository.Save();
        return OperationResult.Success();
    }
}
