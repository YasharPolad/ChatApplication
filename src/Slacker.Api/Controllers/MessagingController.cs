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

namespace Slacker.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MessagingController : BaseController
{
    public MessagingController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
    {
    }

    [HttpPost("create-post")]
    [Authorize]
    public async Task<IActionResult> CreatePost(CreatePost request, IFormFile file)
    {
        var command = _mapper.Map<CreatePostCommand>(request);
        command.CreatingUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess == true
            ? Ok("Post is created!")
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }
}
