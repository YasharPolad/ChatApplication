using Microsoft.EntityFrameworkCore;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Interfaces;
public interface ISlackerDbContext //TODO: Shouldn't the interface be ORM-agnostic? Should work with EF, dapper, or other orm.
{
    DbSet<Connection> Connections { get; }
    DbSet<Employee> Employees { get; }
    DbSet<Post> Posts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
