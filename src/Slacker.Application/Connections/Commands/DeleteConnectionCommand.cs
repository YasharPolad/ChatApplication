using MediatR;
using Slacker.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.Commands;
public class DeleteConnectionCommand : IRequest<BaseMediatrResult>
{
    public int Id { get; set; }
}
