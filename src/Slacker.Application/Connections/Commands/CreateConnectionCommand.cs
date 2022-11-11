using MediatR;
using Slacker.Application.Models;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.Commands;
public class CreateConnectionCommand : IRequest<MediatrResult<Connection>>
{
    public string CreatingUserId { get; set; }
    public string Name { get; set; }
    public bool IsChannel { get; set; }
    public bool IsPrivate { get; set; }
}
