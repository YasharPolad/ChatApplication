using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure;
public class SlackerDbContext : IdentityDbContext<IdentityUser>
{
	public SlackerDbContext(DbContextOptions options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<IdentityRole>().HasData(
			new IdentityRole { Name = "Default", NormalizedName = "Default"},
			new IdentityRole { Name = "Manager", NormalizedName = "Manager"},
			new IdentityRole { Name = "Admin", NormalizedName = "Admin"});
	}


}
