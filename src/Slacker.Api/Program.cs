using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Slacker.Api.Authorization;
using Slacker.Api.Filters;
using Slacker.Api.Hubs;
using Slacker.Api.Middlewares;
using Slacker.Application.Users.Commands;
using Slacker.Infrastructure;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.AddInfrastructure();

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


var app = builder.Build();

var seeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<DatabaseSeeder>(); //TODO: how does this work exactly?
await seeder.Seed();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseGlobalExceptionHandler();

app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<SlackerHub>("/chat");

app.Run();
