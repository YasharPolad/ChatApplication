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
internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, BaseMediatrResult>
{
    private readonly IIdentityService _identity;

    public ConfirmEmailCommandHandler(IIdentityService identity)
    {
        _identity = identity;
    }

    public async Task<BaseMediatrResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        return await _identity.ConfirmEmailAsync(request.Token, request.Email);      
    }
}
