using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Episodes", Schema = "course")]
public class EpisodeQueryModel : BaseEntity
{
    public Guid SectionId { get;  set; }
    public string Title { get;  set; }
    public string EnglishTitle { get; set; }
    public Guid Token { get;  set; }
    public TimeSpan TimeSpan { get;  set; }
    public string VideoName { get;  set; }
    public string? AttachmentName { get;  set; }
    public bool IsActive { get; set; }
    public bool IsFree { get; set; }
    [ForeignKey("SectionId")]
    public SectionQueryModel Section { get; set; }
}
