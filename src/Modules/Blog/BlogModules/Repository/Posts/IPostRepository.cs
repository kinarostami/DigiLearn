using BlogModules.Context;
using BlogModules.Domain;
using Common.Domain.Repository;
using Common.Infrastructure.Repository;

namespace BlogModules.Repository.Posts;

interface IPostRepository : IBaseRepository<Post>
{
}
class PostRepository : BaseRepository<Post, BlogContext>, IPostRepository
{
    public PostRepository(BlogContext context) : base(context)
    {
    }
}
