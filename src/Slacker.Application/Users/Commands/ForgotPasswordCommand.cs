using MediatR;
using Slacker.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Users.Commands;
public class ForgotPasswordCommand : IRequest<BaseMediatrResult>
{
    public string Email { get; set; }
}
