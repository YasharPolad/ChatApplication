using MediatR;
using Slacker.Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.Commands;
public class UpdateConnectionCommand : IRequest<BaseMediatrResult>
{
    public int ConnectionId { get; set; }
    public bool IsPrivate { get; set; } 
    public string Name { get; set; }
}
