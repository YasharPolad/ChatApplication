﻿using MediatR;
using Slacker.Application.Models;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Posts.Queries;
public class GetRepliesByPostQuery : IRequest<MediatrResult<List<Post>>>
{
    public int postId { get; set; }
}
