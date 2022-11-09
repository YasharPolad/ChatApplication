using AutoMapper;
using MediatR;
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

namespace Slacker.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IEmailService _emailService;

    public AuthController(IMapper mapper, IMediator mediator, IEmailService emailService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _emailService = emailService;
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

        if (mediatrResponse.IsSuccess) //TODO: Needs to be refactored. This shouldn't be in the controller. Should be able to resend new token
        {
            var token = mediatrResponse.EmailConfirmationToken;
            var email = mediatrResponse.UserEmail;
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email }, Request.Scheme);
            var response = await _emailService.SendMailAsync(email, "Email Confirmation", $"Click this link to confirm your email: {confirmationLink}");
            return Ok();
        }
        
        else
            return BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse)); //TODO: Automatically return different error responses depending on the error code
        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest login)
    {
        if (!ModelState.IsValid)
        {

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

    [HttpGet("email-confirm")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var command = new ConfirmEmailCommand
        {
            Token = token,
            Email = email
        };
        var mediatrResponse = await _mediator.Send(command);
        if (mediatrResponse.IsSuccess)
        {
            return Ok("You can login now");
        }
        else
        {
            return BadRequest(_mapper.Map<ErrorResponse>(mediatrResponse));
        }
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var command = new ForgotPasswordCommand { Email = request.Email };
        var mediatrResponse = await _mediator.Send(command);

        if (mediatrResponse.IsSuccess)
        {
            return Ok("Your email has been sent");
        }
        else
        {
            return BadRequest(mediatrResponse.Errors[0]);
        }
    }

    [HttpGet("reset-password")]
    public async Task<IActionResult> ResetPassword(string passwordResetToken, string email)
    
    {
        return Ok();
    }
}
