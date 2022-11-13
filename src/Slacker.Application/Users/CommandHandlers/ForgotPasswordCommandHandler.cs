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
internal class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, BaseMediatrResult>
{
    private readonly IIdentityService _identity;

    public ForgotPasswordCommandHandler(IIdentityService identity)
    {
        _identity = identity;
    }
    public async Task<BaseMediatrResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _identity.ForgotPasswordAsync(request.Email);
    }
}
