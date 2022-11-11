using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.Connection;
using Slacker.Application.Connections.Commands;

namespace Slacker.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ConnectionController : BaseController
{
    public ConnectionController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
    {
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateConnection(CreateConnection request)
    {
        var command = _mapper.Map<CreateConnectionCommand>(request);
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess == true
            ? Ok(mediatrResponse.Payload)
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse)); 
    }
}
