using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.User.Request;
using Slacker.Api.Contracts.User.Response;
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
        var mediatrResponse = await _mediator.Send(command);

        if (mediatrResponse.IsSuccess) 
            return Ok();
        else
            return BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse)); //TODO: Automatically return different error responses depending on the error code
        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest login)
    {
        if (!ModelState.IsValid)
        {
            //TODO: Turn model state error into our restful format

            var result = new ErrorResponse();
            ModelState.ToList().ForEach(error => result.Errors.Add(error.Value.ToString()));
            return BadRequest(result);

        }

        var command = _mapper.Map<LoginCommand>(login);
        var mediatrResponse = await _mediator.Send(command);

        if(mediatrResponse.IsSuccess)
        {
            return Ok(_mapper.Map<LoginResponse>(mediatrResponse));
        }
        else
        {
            return BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse)); 
        }

    }
}
