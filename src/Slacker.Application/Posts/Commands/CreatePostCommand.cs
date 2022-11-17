using MediatR;
using Microsoft.AspNetCore.Http;
using Slacker.Application.Models;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Posts.Commands;
public class CreatePostCommand : IRequest<MediatrResult<Post>>
{
    public int ConnectionId { get; set; }
    public string Message { get; set; }
    public string CreatingUserId { get; set; }
    public List<IFormFile>? Files { get; set; }
    public int? ParentPost { get; set; }
}
