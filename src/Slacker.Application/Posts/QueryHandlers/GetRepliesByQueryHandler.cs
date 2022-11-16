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
internal class GetRepliesByQueryHandler : IRequestHandler<GetRepliesByPostQuery, MediatrResult<List<Post>>>
{
    private readonly ISlackerDbContext _context;

    public GetRepliesByQueryHandler(ISlackerDbContext context)
    {
        _context = context;
    }

    public async Task<MediatrResult<List<Post>>> Handle(GetRepliesByPostQuery request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<List<Post>>();

        var replies = await _context.Posts.Include(p => p.Reactions)
                                    .Where(p => p.ParentPostId == request.postId)
                                    .ToListAsync();

        result.IsSuccess = true;
        result.Payload = replies;
        return result;
    }
}
