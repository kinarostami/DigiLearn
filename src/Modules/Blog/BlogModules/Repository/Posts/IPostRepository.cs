using BlogModules.Context;
using BlogModules.Domain;
using Common.Domain.Repository;
using Common.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogModules.Repository.Posts;

interface IPostRepository : IBaseRepository<Post>
{
    void Delete(Post post);
    Task<List<Post>> GetAll();
}
class PostRepository : BaseRepository<Post, BlogContext>, IPostRepository
{
    public PostRepository(BlogContext context) : base(context)
    {
    }
    public void Delete(Post post)
    {
        Context.Posts.Remove(post);
    }

    public async Task<List<Post>> GetAll()
    {
        return await Context.Posts.ToListAsync();
    }
}
