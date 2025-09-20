using Common.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogModules.Domain;

[Index("Slg",IsUnique = true)]
[Table("Posts",Schema = "dbo")]
class Post : BaseEntity
{
    [MaxLength(80)]
    public string Title { get; set; }
    public Guid UserId { get; set; }

    [MaxLength(80)]
    public string OwnerName { get; set; }
    public string Description { get; set; }
    
    [MaxLength(80)]
    public string Slug { get; set; }

    [MaxLength(110)]
    public string ImageName { get; set; }
    public long Visit { get; set; }
    public Guid CategoryId { get; set; }
}
