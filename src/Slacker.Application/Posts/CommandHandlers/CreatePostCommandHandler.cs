using MediatR;
using Microsoft.EntityFrameworkCore;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Models;
using Slacker.Application.Posts.Commands;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Posts.CommandHandlers;
internal class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, MediatrResult<Post>>
{
    private readonly IPostRepository _postRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IFileHandlerService _fileHandlerService;

    public CreatePostCommandHandler(IPostRepository postRepository, IEmployeeRepository employeeRepository, IFileHandlerService fileHandlerService)
    {
        _postRepository = postRepository;
        _employeeRepository = employeeRepository;
        _fileHandlerService = fileHandlerService;
    }

    public async Task<MediatrResult<Post>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<Post>();

        var creatingEmployee = await _employeeRepository.GetAsync(e => e.IdentityId == request.CreatingUserId);
        
        Post newPost = new Post
        {
            EmployeeId = creatingEmployee is null ? 0 : creatingEmployee.Id,
            Message = request.Message,
            ConnectionId = request.ConnectionId,
            CreatedAt = DateTime.UtcNow, //TODO: CreatedAt should be assigned automatically
            ParentPostId = request.ParentPost
        };

        if(request.File != null)
        {
            var filePath = await _fileHandlerService.SaveFile(request.File);
            if (filePath != null)
            {
                newPost.Attachments.Add(
                    new Attachment
                    {
                        FileName = Path.GetFileName(filePath),
                        Post = newPost,
                        Type = 1, //TODO: DO SOMETHING ABOUT FILE TYPES
                    }
                );
            }
        }

        await _postRepository.CreateAsync(newPost); //TODO: Maybe I should have custom exceptions for create method, cause it doesn't return a result object, by which I could check whether the operation was successful
        result.IsSuccess = true;
        result.Payload = newPost;
        return result;
    }
}
