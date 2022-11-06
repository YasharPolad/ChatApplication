using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slacker.Application.Interfaces;
using Slacker.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure;
public static class ConfigureServices
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {

        builder.Services.AddDbContext<SlackerDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
        });

        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 5;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;          
        }).AddEntityFrameworkStores<SlackerDbContext>().AddDefaultTokenProviders();

        builder.Services.AddTransient<IIdentityService, IdentityService>();
        
        
        return builder;
    }
}
