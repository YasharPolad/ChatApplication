using MediatR;
using Microsoft.EntityFrameworkCore;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
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
    private readonly IPostRepository _postRepository;

    public GetPostsByConnectionQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<MediatrResult<List<Post>>> Handle(GetPostsByConnectionQuery request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<List<Post>>();
        List<Post> posts = await _postRepository.GetAllAsync(p => p.ConnectionId == request.ConnectionId && p.ParentPost == null, 
                                                             p => p.ChildPosts, p => p.Reactions);

        result.Payload = posts;
        result.IsSuccess = true;
        return result;
    }
}
