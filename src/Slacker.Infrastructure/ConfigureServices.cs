using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Infrastructure.ConfigOptions;
using Slacker.Infrastructure.Repositories;
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
        builder.Services.AddScoped<ISlackerDbContext, SlackerDbContext>();

        //Repositories
        builder.Services.AddScoped<IConnectionRepository, ConnectionRepository>(); //TODO: Maybe move all repositories to another file
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();



        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 5;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;          
        }).AddEntityFrameworkStores<SlackerDbContext>().AddDefaultTokenProviders();

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

        builder.Services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,

                ValidAudience = builder.Configuration["JwtSettings:Issuer"],
                ValidIssuer = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
            };
        });

        builder.Services.AddSendGrid(options =>
        {
            options.ApiKey = builder.Configuration["SendgridSettings:ApiKey"];
        });
        builder.Services.AddScoped<IEmailService, SendGridService>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddScoped<DatabaseSeeder>();
        
        return builder;
    }
}
