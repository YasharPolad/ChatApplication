using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.Connection.Requests;
using Slacker.Api.Contracts.Connection.Responses;
using Slacker.Application.Connections.Commands;
using Slacker.Application.Connections.Queries;
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
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteConnection(int id) //TODO: Who can delete a connection?
    {
        var command = new DeleteConnectionCommand { Id = id };
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess == true
            ? Ok($"Connection with ID number {id} has been deleted")
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateConnection(int id, [FromBody] UpdateConnection request)
    {
        var command = _mapper.Map<UpdateConnectionCommand>(request);
        command.ConnectionId = id;
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess == true
            ? Ok($"Connection with ID number {id} has been updated")
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetChannelsByEmployee(int employeeId)
    {
        var query = new GetChannelsByEmployeeQuery { EmployeeId = employeeId };
        var mediatrResponse = await _mediator.Send(query);

        return mediatrResponse.IsSuccess == true
            ? Ok(_mapper.Map<List<ConnectionResponse>>(mediatrResponse.Payload))  //TODO: LEARN MAPPING WELL
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

    [HttpGet("messages")]
    [Authorize]
    public async Task<IActionResult> GetDirectMessagesByEmployee(int employeeId)
    {
        var query = new GetDirectMessagesByEmployeeQuery { EmployeeId = employeeId };
        var mediatrResponse = await _mediator.Send(query);

        return mediatrResponse.IsSuccess == true
            ? Ok(_mapper.Map<List<ConnectionResponse>>(mediatrResponse.Payload))  //TODO: LEARN MAPPING WELL
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }
}
