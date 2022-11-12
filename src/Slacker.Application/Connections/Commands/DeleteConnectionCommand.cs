using MediatR;
using Slacker.Application.Models.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.Commands;
public class DeleteConnectionCommand : IRequest<DeleteConnectionMediatrResult>
{
    public int Id { get; set; }
}
