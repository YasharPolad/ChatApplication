using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Slacker.Application.Interfaces;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure;
public class SlackerDbContext : IdentityDbContext<IdentityUser>, ISlackerDbContext
{
	public SlackerDbContext(DbContextOptions options) : base(options)
	{

	}

	public DbSet<Connection> Connections => Set<Connection>();
	public DbSet<Employee> Employees => Set<Employee>();
	public DbSet<Post> Posts => Set<Post>(); //TODO: Delete reply posts recursively when a post is delete. Maybe in interception?

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}


}
