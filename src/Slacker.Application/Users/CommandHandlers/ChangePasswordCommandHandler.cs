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
internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordMediatrResult>
{
    private readonly IIdentityService _identity;

    public ChangePasswordCommandHandler(IIdentityService identity)
    {
        _identity = identity;
    }

    public async Task<ChangePasswordMediatrResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        return await _identity.ChangePassword(request.OldPassword, request.NewPassword, request.Email);
    }
}
