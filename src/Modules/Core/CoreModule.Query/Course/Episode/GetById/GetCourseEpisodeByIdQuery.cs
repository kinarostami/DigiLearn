using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query._Data.Entities;
using CoreModule.Query.Course._DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Course.Episode.GetById;

public record GetCourseEpisodeByIdQuery(Guid EpisodeId) : IQuery<EpisodeDto?>
{

}
public record GetCourseEpisodeByIdQueryHandler : IQueryHandler<GetCourseEpisodeByIdQuery, EpisodeDto?>
{
    private readonly QueryContext _context;

    public GetCourseEpisodeByIdQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<EpisodeDto?> Handle(GetCourseEpisodeByIdQuery request, CancellationToken cancellationToken)
    {
        var episode = await _context.Episodes.FirstOrDefaultAsync(x => x.Id == request.EpisodeId);
        if (episode == null)
            return null;

        return new EpisodeDto()
        {
            Id = episode.Id,
            AttachmentName = episode.AttachmentName,
            CreationDate = episode.CreationDate,
            SectionId = episode.SectionId,
            EnglishTitle = episode.EnglishTitle,
           Title = episode.Title,
           TimeSpan = episode.TimeSpan,
           Token = episode.Token,
           IsActive = episode.IsActive,
           IsFree   = episode.IsFree,
           VideoName = episode.VideoName,
        };
    }
}