﻿using MediatR;
using Slacker.Application.Interfaces;
using Slacker.Application.Models.User;
using Slacker.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Users.CommandHandlers;
internal class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordMediatrResult>
{
    private readonly IIdentityService _identity;

    public ForgotPasswordCommandHandler(IIdentityService identity)
    {
        _identity = identity;
    }
    public async Task<ForgotPasswordMediatrResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _identity.ForgotPasswordAsync(request.Email);
    }
}
