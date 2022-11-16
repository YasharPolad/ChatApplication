using MediatR;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Models;
using Slacker.Application.Posts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Posts.CommandHandlers;
internal class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, BaseMediatrResult>
{
    private readonly IPostRepository _postRepository;

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<BaseMediatrResult> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseMediatrResult();
        var post = await _postRepository.GetAsync(p => p.Id == request.Id);
        
        if(post is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("This post doesn't exist");
            return result;
        }

        await _postRepository.DeleteAsync(post);
        result.IsSuccess = true;
        return result;
    }
}
