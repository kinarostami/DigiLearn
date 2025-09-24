using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Categories")]
public class CategoryQueryModel : BaseEntity
{
    public Guid? ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }

    [ForeignKey("ParentId")]
    public List<CategoryQueryModel> Childs { get; set; }
}
