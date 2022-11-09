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
internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordMediatrResult>
{
    private readonly IIdentityService _identityService;

    public ResetPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ResetPasswordMediatrResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.ResetPasswordAsync(request.NewPassword, request.Email, request.Token);
           
    }
}
