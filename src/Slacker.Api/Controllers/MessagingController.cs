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
using Slacker.Application.Posts.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
    public async Task<IActionResult> CreatePost([FromForm]CreatePost request) //TODO: Maybe need a seperate controller for replies. A reply should be able to assign a connection to itself
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

    [HttpDelete("{id}")]
    [Authorize(policy: "OnlyPostCreatorCanEdit")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var command = new DeletePostCommand { Id = id };
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess == true
            ? Ok("Post is deleted")
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

    [HttpGet("posts")]
    [Authorize]
    public async Task<IActionResult> GetPostsByConnection(int connectionId)
    {
        var query = new GetPostsByConnectionQuery { ConnectionId = connectionId};
        var mediatrResponse = await _mediator.Send(query);

        return mediatrResponse.IsSuccess == true
            ? Ok(_mapper.Map<List<GetPostResponse>>(mediatrResponse.Payload))
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

    [HttpGet("replies")]
    [Authorize]
    public async Task<IActionResult> GetRepliesByPost(int postId)
    {
        var query = new GetRepliesByPostQuery { postId = postId };
        var mediatrResponse = await _mediator.Send(query);

        return mediatrResponse.IsSuccess == true
            ? Ok(_mapper.Map<List<GetReplyResponse>>(mediatrResponse.Payload))
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

    [HttpGet("download-file")]
    [Authorize]
    public async Task<IActionResult> DownloadFile(int attachmentId)
    {
        var query = new GetAttachmentQuery { AttachmentId= attachmentId };
        var mediatrResponse = await _mediator.Send(query);

        Response.Headers.Add("Content-Disposition", $"attachment;filename={mediatrResponse.Payload.FileName}");

        return mediatrResponse.IsSuccess == true
            ? File(mediatrResponse.Payload.FileStream, mediatrResponse.Payload.ContentType)
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

    //TODO: Implement add/remove reaction to post

}
