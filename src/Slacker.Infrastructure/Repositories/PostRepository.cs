using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.Repositories;
public class PostRepository : GenericRepository<Post, SlackerDbContext>, IPostRepository
{
    public PostRepository(SlackerDbContext context) : base(context)
    {
    }

}
