using MediatR;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Models;
using Slacker.Application.Posts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Posts.CommandHandlers;
internal class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, BaseMediatrResult>
{
    private readonly IPostRepository _postRepository;

    public UpdatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<BaseMediatrResult> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseMediatrResult();

        var post = await _postRepository.GetAsync(p => p.Id == request.postId);
        if(post == null)
        {
            result.IsSuccess = false;
            result.Errors.Add("This post doesn't exist");
            return result;
        }

        post.Message = request.updatedMessage;
        await _postRepository.UpdateAsync(post); //TODO: maybe have this in a try-catch block and raise custom exception

        result.IsSuccess = true;
        return result;
    }
}
