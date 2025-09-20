using BlogModules.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogModules.Context;

class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> option) : base(option)
    {
        
    }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
}
