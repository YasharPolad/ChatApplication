using MediatR;
using Slacker.Application.Interfaces;
using Slacker.Application.Models;
using Slacker.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Users.CommandHandlers;
internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, BaseMediatrResult>
{
    private readonly IIdentityService _identity;

    public ChangePasswordCommandHandler(IIdentityService identity)
    {
        _identity = identity;
    }

    public async Task<BaseMediatrResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        return await _identity.ChangePassword(request.OldPassword, request.NewPassword, request.Email);
    }
}
