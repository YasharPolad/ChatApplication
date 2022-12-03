using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Slacker.Api.Authorization;
using Slacker.Api.Filters;
using Slacker.Application.Users.Commands;
using System.Runtime.CompilerServices;

namespace Slacker.Api;

public static class ConfigureServices
{
    public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;  //suppress default modelstate validation
        });

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(new ValidateInput());  //globally apply modelstate validation
        });
        builder.Services.AddAutoMapper(typeof(Program), typeof(RegisterRequestCommand));
        builder.Services.AddMediatR(typeof(Program), typeof(RegisterRequestCommand));
        builder.Services.AddLogging();

        builder.Services.AddScoped<IAuthorizationHandler, PostModifyRequirementHandler>();
        builder.Services.AddScoped<IAuthorizationHandler, PostCreateRequirementHandler>();

        builder.Services.AddAuthorization(configure =>
        {
            configure.AddPolicy("OnlyPostCreatorCanEdit", policy => policy
                    .Requirements.Add(new PostModifyRequirement()));

            configure.AddPolicy("OnlyConnectionMemberCanPost", policy => policy
                    .Requirements.Add(new PostCreateRequirement()));
        });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                      .SetIsOriginAllowed(origin => true);
            });
        });


        builder.Services.AddSignalR();

        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Bearer Authentication with JWT Token",
                Type = SecuritySchemeType.Http
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });

        builder.Host.UseSerilog((ctx, lc) =>
        {
            lc.ReadFrom.Configuration(ctx.Configuration);
        });
           

        return builder;
    }
}
