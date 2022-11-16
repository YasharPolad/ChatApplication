using MediatR;
using Slacker.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Posts.Commands;
public class DeletePostCommand : IRequest<BaseMediatrResult>
{
    public int Id { get; set; }
}
