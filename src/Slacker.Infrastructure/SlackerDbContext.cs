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

	 
}
