using CommentModules.Domain;
using Common.Domain;
using Common.Query.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentModules.Services.DTOs;

public class CreateCommentCommand : BaseEntity
{
    [Required]
    public string Text { get; set; }
    public Guid? ParentId { get; set; } = null;
    public Guid UserId { get; set; } = Guid.Empty;
    public Guid EntityId { get; set; }
    public CommentType CommentType { get; set; }
}
public class CommentDto
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string Text { get; set; }
    public Guid UserId { get; set; }
    public Guid EntityId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public CommentType CommentType { get; set; }

    [ForeignKey("ParentId")]
    public List<CommentReplyDto> Replies { get; set; }
}
public class CommentReplyDto
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string Text { get; set; }
    public Guid? ParentId { get; set; }
    public Guid UserId { get; set; }
    public Guid EntityId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public CommentType CommentType { get; set; }
}
public class CommentFilterResult : BaseFilter<CommentDto>
{
    public CommentFilterParams FilterParams { get; set; }
}
public class AllCommentFilterReulst : BaseFilter<CommentReplyDto>
{
    
}
public class CommentFilterParams : BaseFilterParam
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public CommentType? CommentType { get; set; } = null;
    public Guid? EntityId { get; set; }
    public string? Name { get; set; }
    public string? Family { get; set; }
}