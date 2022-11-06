using MediatR;
using Slacker.Application.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Users.Commands;
public class LoginCommand : IRequest<LoginMediatrResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
