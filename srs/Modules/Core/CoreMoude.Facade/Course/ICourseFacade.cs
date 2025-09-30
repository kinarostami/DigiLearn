using Common.Application;
using CoreModule.Application.Category.Create;
using CoreModule.Application.Category.Edit;
using CoreModule.Application.Course.Create;
using CoreModule.Application.Course.Edit;
using CoreModule.Application.Course.Episodes.Accept;
using CoreModule.Application.Course.Episodes.Add;
using CoreModule.Application.Course.Episodes.Delete;
using CoreModule.Application.Course.Episodes.Edit;
using CoreModule.Application.Course.Sections.AddSection;
using CoreModule.Query.Course._DTOs;
using CoreModule.Query.Course.Episode.GetById;
using CoreModule.Query.Course.GetByFilter;
using CoreModule.Query.Course.GetById;
using CoreModule.Query.Course.GetBySlug;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModue.Facade.Course;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);
    Task<OperationResult> Edit(EditCourseCommand command);
    Task<OperationResult> AddSection(AddCourseSectionCommand command);
    Task<OperationResult> AddEpisodes(AddCourseEpisode command);
    Task<OperationResult> AcceptEpisode(AcceptEpisodeCommand command);
    Task<OperationResult> DeleteEpisode(DeleteEpisodeCommand command);
    Task<OperationResult> EditEpisode(EditCourseEpisodeCommand command);

    Task<CourseFilterResult> GetCourseFilter(CourseFilterParams param);
    Task<CourseDto?> GetCourseById(Guid id);
    Task<EpisodeDto?> GetCourseEpisodeById(Guid episodeId);
    Task<CourseDto?> GetCourseBySlug(string slug);
}
public class CourseFacade : ICourseFacade
{
    private readonly IMediator _mediator;

    public CourseFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AcceptEpisode(AcceptEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddEpisodes(AddCourseEpisode command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddSection(AddCourseSectionCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteEpisode(DeleteEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditEpisode(EditCourseEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CourseDto?> GetCourseById(Guid id)
    {
        return await _mediator.Send(new GetCourseByIdQuery(id));
    }

    public async Task<CourseDto?> GetCourseBySlug(string slug)
    {
        return await _mediator.Send(new GetCourseBySlugQuery(slug));
    }

    public async Task<EpisodeDto?> GetCourseEpisodeById(Guid episodeId)
    {
        return await _mediator.Send(new GetCourseEpisodeByIdQuery(episodeId));
    }

    public async Task<CourseFilterResult> GetCourseFilter(CourseFilterParams param)
    {
        return await _mediator.Send(new GetCoursesByFilterQuery(param));
    }
}
