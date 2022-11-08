using MediatR;
using Slacker.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Users.Commands;
public class ForgotPasswordCommand : IRequest<ForgotPasswordMediatrResult>
{
    public string Email { get; set; }
}
