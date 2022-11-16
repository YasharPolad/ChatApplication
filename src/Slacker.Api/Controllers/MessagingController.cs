using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slacker.Api.Contracts.Connection.Responses;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.Posts.Request;
using Slacker.Application.Posts.Commands;
using System.Security.Claims;
using Slacker.Api.Contracts.Posts.Response;

namespace Slacker.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MessagingController : BaseController
{
    public MessagingController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
    {
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePost([FromForm]CreatePost request)
    {
        var command = _mapper.Map<CreatePostCommand>(request);
        command.CreatingUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess == true
            ? Ok(_mapper.Map<CreatePostResponse>(mediatrResponse.Payload))     
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

    [HttpPut("{id}")]
    [Authorize(policy: "OnlyPostCreatorCanEdit")]
    public async Task<IActionResult> EditPost(int id, [FromBody] UpdatePost request)
    {
        var command = _mapper.Map<UpdatePostCommand>(request);
        command.postId = id;
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess == true
            ? Ok("Post is updated")
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

}
