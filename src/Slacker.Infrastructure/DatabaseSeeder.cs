using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure;
public class DatabaseSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseSeeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task Seed()
    {
        if(_roleManager.Roles.ToList().Count == 0)
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "Default", NormalizedName = "Default" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "Manager", NormalizedName = "Manager" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "Admin" });
        };
            
    }
}
