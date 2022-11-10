using Microsoft.EntityFrameworkCore;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Interfaces;
public interface ISlackerDbContext
{
    public DbSet<Connection> Connections { get; }
    public DbSet<Employee> Employees { get; }
    public DbSet<Post> Posts { get; }
}
