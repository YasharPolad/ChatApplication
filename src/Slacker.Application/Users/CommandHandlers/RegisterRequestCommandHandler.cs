using MediatR;
using Slacker.Application.Interfaces;
using Slacker.Application.Models.User;
using Slacker.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Users.CommandHandlers;
internal class RegisterRequestCommandHandler : IRequestHandler<RegisterRequestCommand, RegisterResponse>
{
    private readonly IIdentityService _identity;

    public RegisterRequestCommandHandler(IIdentityService identity)
    {
        _identity = identity;
    }

    public async Task<RegisterResponse> Handle(RegisterRequestCommand request, CancellationToken cancellationToken)
    {
        return await _identity.RegisterUserAsync(request.Email, request.Password, request.PhoneNumber); 
    }
}
