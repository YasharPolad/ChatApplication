using MediatR;
using Slacker.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Users.Commands;
public class ConfirmEmailCommand : IRequest<ConfirmEmailMediatrResult>
{
    public string Token { get; set; }
    public string Email { get; set; }
}
