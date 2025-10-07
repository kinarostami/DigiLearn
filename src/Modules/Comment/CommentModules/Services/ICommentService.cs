using AutoMapper;
using CommentModules.Context;
using CommentModules.Domain;
using CommentModules.Services.DTOs;
using Common.Application;
using Common.Application.SecurityUtil;
using Common.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentModules.Services;

public interface ICommentService
{
    Task<OperationResult> CreateComment(CreateCommentCommand command);
    Task<OperationResult> DeleteComment(Guid id);


    Task<CommentDto?> GetCommentById(Guid id);
    Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams filterParams);
    Task<AllCommentFilterReulst> GetAllComments(CommentFilterParams filterParams);
}
public class CommentService : ICommentService
{
    private readonly CommentContext _commentContext;
    private readonly IMapper _mapper;
    public CommentService(CommentContext commentContext, IMapper mapper)
    {
        _commentContext = commentContext;
        _mapper = mapper;
    }

    public async Task<OperationResult> CreateComment(CreateCommentCommand command)
    {
        var comment = _mapper.Map<Comment>(command);

        comment.Text = command.Text.SanitizeText();
        comment.IsActive = true;
        command.Id = Guid.NewGuid();
        _commentContext.Add(comment);
        await _commentContext.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteComment(Guid id)
    {
        var comment = await _commentContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
            return OperationResult.NotFound();

        _commentContext.Remove(comment);
        await _commentContext.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<CommentDto?> GetCommentById(Guid id)
    {
        var comment = await _commentContext.Comments
            .Include(x => x.User)
            .Include(x => x.Replise)
            .ThenInclude(x => x.User ).FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
            return null;

        return new CommentDto()
        {
            Id = comment.Id,
            CommentType = comment.CommentType,
            CreationDate = comment.CreationDate,
            EntityId = comment.EntityId,
            FullName = $"{comment.User.Name} {comment.User.Family}",
            IsActive = comment.IsActive,
            Text = comment.Text,
            UserId = comment.UserId,
            Email = comment.User.Email.SetUnReadableEmail(),
            Replies = comment.Replise.Select(x => new CommentReplyDto
            {
                Id = x.Id,
                CommentType = x.CommentType,
                CreationDate = x.CreationDate,
                EntityId = x.EntityId,
                ParentId = x.ParentId,
                FullName = $"{x.User.Name} {x.User.Family}",
                IsActive = x.IsActive,
                Text = x.Text,
                UserId = x.UserId,
                Email = x.User.Email.SetUnReadableEmail()
            }).ToList()
        };

       
    }

    public async Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams filterParams)
    {
        var query = _commentContext.Comments
           .Include(x => x.Replise)
           .ThenInclude(x => x.User)
           .Include(x => x.User)
           .Where(x => x.ParentId == null)
           .OrderByDescending(x => x.CreationDate).AsQueryable();

        if (filterParams.CommentType != null)
           query = query.Where(x => x.CommentType == filterParams.CommentType);

        if (filterParams.EntityId != null)
           query = query.Where(x => x.EntityId == filterParams.EntityId);

        if (filterParams.StartDate != null)
           query = query.Where(x => x.CreationDate.Date >= filterParams.StartDate.Value.Date);

        if (filterParams.EndDate != null)
            query = query.Where(x => x.CreationDate.Date <= filterParams.EndDate.Value.Date);

        if (filterParams.Name != null)
           query = query.Where(x => x.User.Name.Contains(filterParams.Name));
        
        if (filterParams.Family != null)
           query = query.Where(x => x.User.Family.Contains(filterParams.Family));

        var skip =(filterParams.PageId - 1) * filterParams.Take;

        var model = new CommentFilterResult()
        {
            Data = await query.Skip(skip).Take(filterParams.Take).Select(x => new CommentDto()
            {
                Id = x.Id,
                CommentType = x.CommentType,
                CreationDate = x.CreationDate,
                EntityId = x.EntityId,
                FullName = $"{x.User.Name} {x.User.Family}",
                IsActive = x.IsActive,
                Text = x.Text,
                UserId = x.UserId,
                Email = x.User.Email.SetUnReadableEmail(),
                Replies = x.Replise.Select(s => new CommentReplyDto
                {
                    Id = s.Id,
                    CommentType = s.CommentType,
                    CreationDate = s.CreationDate,
                    EntityId = s.EntityId,
                    ParentId = s.ParentId,
                    FullName = $"{s.User.Name} {s.User.Family}",
                    IsActive = s.IsActive,
                    Text = s.Text,
                    UserId = s.UserId,
                    Email = s.User.Email.SetUnReadableEmail()
                }).ToList()
            }).ToListAsync(),
            FilterParams = filterParams
        };
        model.GeneratePaging(query, filterParams.Take, filterParams.PageId);
        return model;
    }

    public async Task<AllCommentFilterReulst> GetAllComments(CommentFilterParams filterParams)
    {
        var query = _commentContext.Comments
           .Include(x => x.Replise)
           .ThenInclude(x => x.User)
           .Include(x => x.User)
           .OrderByDescending(x => x.CreationDate).AsQueryable();

        if (filterParams.CommentType != null)
            query = query.Where(x => x.CommentType == filterParams.CommentType);

        if (filterParams.EntityId != null)
            query = query.Where(x => x.EntityId == filterParams.EntityId);

        if (filterParams.StartDate != null)
            query = query.Where(x => x.CreationDate.Date >= filterParams.StartDate.Value.Date);

        if (filterParams.EndDate != null)
            query = query.Where(x => x.CreationDate.Date <= filterParams.EndDate.Value.Date);

        if (filterParams.Name != null)
            query = query.Where(x => x.User.Name.Contains(filterParams.Name));

        if (filterParams.Family != null)
            query = query.Where(x => x.User.Family.Contains(filterParams.Family));

        var skip = (filterParams.PageId - 1) * filterParams.Take;

        var model = new AllCommentFilterReulst()
        {
            Data = await query.Skip(skip).Take(filterParams.Take)
            .Select(s => new CommentReplyDto
                {
                    Id = s.Id,
                    CommentType = s.CommentType,
                    CreationDate = s.CreationDate,
                    EntityId = s.EntityId,
                    ParentId = s.ParentId,
                    FullName = $"{s.User.Name} {s.User.Family}",
                    IsActive = s.IsActive,
                    Text = s.Text,
                    UserId = s.UserId,
                    Email = s.User.Email
                }).ToListAsync()
        };
        model.GeneratePaging(query, filterParams.Take, filterParams.PageId);
        return model;
    }
}
