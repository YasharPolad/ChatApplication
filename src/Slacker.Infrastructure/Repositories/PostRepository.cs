using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.Repositories;
public class PostRepository : GenericRepository<Post, SlackerDbContext>, IPostRepository
{
    public PostRepository(SlackerDbContext context) : base(context)
    {
    }

    public async override Task DeleteAsync(Post entity) //Still able to add replies to a "deleted" post, but this fits the Slack model perfectly
    {
        _context.Posts.Attach(entity);
        entity.isActive = false;
        await _context.SaveChangesAsync();
    }

}
