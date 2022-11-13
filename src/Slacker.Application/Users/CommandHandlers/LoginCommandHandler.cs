using MediatR;
using Slacker.Application.Interfaces;
using Slacker.Application.Models;
using Slacker.Application.Models.DTOs;
using Slacker.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Users.CommandHandlers;
internal class LoginCommandHandler : IRequestHandler<LoginCommand, MediatrResult<LoginResponseDto>>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<MediatrResult<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.LoginUserAsync(request.Email, request.Password);
    }
}
