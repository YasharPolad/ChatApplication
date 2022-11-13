using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.User.Request;
using Slacker.Api.Contracts.User.Response;
using Slacker.Api.Filters;
using Slacker.Application.Interfaces;
using Slacker.Application.Users.Commands;
using Slacker.Infrastructure.Services;
using System;
using System.Net.Sockets;
using System.Security.Claims;

namespace Slacker.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    public AuthController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest register)
    {
        
        var command = _mapper.Map<RegisterRequestCommand>(register);
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess == true
            ? Ok("Registration Successful")
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse)); //TODO: Automatically return different error responses depending on the error code
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest login)
    {

        var command = _mapper.Map<LoginCommand>(login);
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess
            ? Ok(_mapper.Map<LoginResponse>(mediatrResponse.Payload))
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));

    }

    [HttpGet("email-confirm")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var command = new ConfirmEmailCommand
        {
            Token = token,
            Email = email
        };
        var mediatrResponse = await _mediator.Send(command);
        
        return mediatrResponse.IsSuccess
            ? Ok("You can login now!")
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

    [HttpPost("forgot-password")] //should this go to a command or query?
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var command = new ForgotPasswordCommand { Email = request.Email };
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess 
            ? Ok("Your email has been sent!") 
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));

    }

    [HttpGet("reset-password")]
    public async Task<IActionResult> ResetPassword(string passwordResetToken, string email)    
    {
        return Ok(new ResetPasswordRequest { Email = email, Token = passwordResetToken});  //the email and password will be passed to the 
    }                                                                                       //js client which will send them inside hidden fields with post request

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {

        var command = _mapper.Map<ResetPasswordCommand>(request);
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess 
            ? Ok("Your password is reset!") 
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var command = _mapper.Map<ChangePasswordCommand>(request);
        command.Email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value; //is this a safe way of extracting user email?
        var mediatrResponse = await _mediator.Send(command);

        return mediatrResponse.IsSuccess
            ? Ok("Your password is changed!")
            : BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
    }
}
