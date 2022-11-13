using MediatR;
using Slacker.Application.Models;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.Commands;
public class AddEmployeeToConnectionCommand : IRequest<BaseMediatrResult>
{
    public int EmployeeId { get; set; }
    public int ConnectionId { get; set; }
}
