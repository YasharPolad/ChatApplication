using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.Connection.Requests;
using Slacker.Api.Contracts.Connection.Responses;
using Slacker.Application.Connections.Commands;
using System.Security.Claims;

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
        command.CreatingUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess == true
            ? Ok(_mapper.Map<ConnectionResponse>(mediatrResponse.Payload))
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse)); 
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> AddEmployeeToConnection(AddEmployeeToConnection request)
    {
        var command = _mapper.Map<AddEmployeeToConnectionCommand>(request);
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess == true
            ? Ok($"Employee with Id {request.EmployeeId} was added to connection {request.ConnectionId}")
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));

    }
}
