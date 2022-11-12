using MediatR;
using Slacker.Application.Models;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.Queries;
public class GetChannelsByEmployeeQuery : IRequest<MediatrResult<List<Connection>>>
{
    public int EmployeeId { get; set; }
}
