using MediatR;
using Slacker.Application.Models;
using Slacker.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Posts.Queries;
public class GetAttachmentQuery : IRequest<MediatrResult<FileDto>>
{
    public int AttachmentId { get; set; }
}
