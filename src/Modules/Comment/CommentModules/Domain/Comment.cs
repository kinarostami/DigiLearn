using Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentModules.Domain;

public class Comment : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid EntityId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Text { get; set; }
    public bool IsActive { get; set; }
    public CommentType CommentType { get; set; }
    public Guid? ParentId { get; set; }

    [ForeignKey("ParentId")]
    public List<Comment> Replise { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}
public enum CommentType
{
    Course = 0,
    Article = 1
}
