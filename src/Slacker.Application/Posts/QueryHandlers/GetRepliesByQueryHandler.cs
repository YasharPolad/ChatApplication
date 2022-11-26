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
internal class GetRepliesByQueryHandler : IRequestHandler<GetRepliesByPostQuery, MediatrResult<List<Post>>>
{
    private readonly IPostRepository _postRepository;

    public GetRepliesByQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<MediatrResult<List<Post>>> Handle(GetRepliesByPostQuery request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<List<Post>>();

        var replies = await _postRepository.GetAllAsync(p => p.ParentPostId == request.postId, p => p.Reactions);

        result.IsSuccess = true;
        result.Payload = replies;
        return result;
    }
}
