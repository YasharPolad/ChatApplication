using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.User.Request;
using Slacker.Application.Users.Commands;

namespace Slacker.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AuthController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest register)
    {
        if(!ModelState.IsValid)
        {
            //TODO: Turn model state error into our restful format
            
            var result = new ErrorResponse();
            ModelState.ToList().ForEach(error => result.Errors.Add(error.Value.ToString()));
            return BadRequest(result);

               
        }
        var command = _mapper.Map<RegisterRequestCommand>(register);
        var response = await _mediator.Send(command);

        if (response.IsSuccess) 
            return Ok();
        else
            return BadRequest(_mapper.Map<ErrorResponse>(response)); //TODO: Automatically return different error responses depending on the error code
        
    }
}
