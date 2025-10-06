using Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace CommentModules.Domain;

[Index("Email",IsUnique =true)]
public class User : BaseEntity
{
    public string? Name { get; set; }
    public string? Family { get; set; }
    public string Avatar { get; set; }
    public string? Email { get; set; }
}
