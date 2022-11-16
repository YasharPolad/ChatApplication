using MediatR;
using Slacker.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Posts.Commands;
public class UpdatePostCommand : IRequest<BaseMediatrResult>
{
    public int postId { get; set; }
    public string updatedMessage { get; set; }
}
