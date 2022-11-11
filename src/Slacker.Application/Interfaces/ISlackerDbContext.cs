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
    public DbSet<Connection> Connections { get; }
    public DbSet<Employee> Employees { get; }
    public DbSet<Post> Posts { get; }
}
