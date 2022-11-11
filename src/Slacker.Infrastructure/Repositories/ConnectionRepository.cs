using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.Repositories;
public class ConnectionRepository : GenericRepository<Connection, SlackerDbContext>, IConnectionRepository
{
    public ConnectionRepository(SlackerDbContext context) : base(context) // _context is protected, can use it here if needed
    {
    }
}
