using MediatR;
using Microsoft.EntityFrameworkCore;
using Slacker.Application.Interfaces;
using Slacker.Application.Models;
using Slacker.Application.Posts.Queries;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Posts.QueryHandlers;
internal class GetPostsByConnectionQueryHandler : IRequestHandler<GetPostsByConnectionQuery, MediatrResult<List<Post>>>
{
    private readonly ISlackerDbContext _context;

    public GetPostsByConnectionQueryHandler(ISlackerDbContext context)
    {
        _context = context;
    }

    public async Task<MediatrResult<List<Post>>> Handle(GetPostsByConnectionQuery request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<List<Post>>();    
        List<Post> posts = await _context.Posts.Include(p => p.Reactions)
                                               .Include(p => p.ChildPosts)
                                               .Where(p => p.ParentPostId == null).ToListAsync(); //Only getting main posts!

        result.Payload = posts;
        result.IsSuccess = true;
        return result;
    }
}
