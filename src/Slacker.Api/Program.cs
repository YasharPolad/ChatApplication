using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Slacker.Api;
using Slacker.Api.Authorization;
using Slacker.Api.Filters;
using Slacker.Api.Hubs;
using Slacker.Api.Middlewares;
using Slacker.Application.Users.Commands;
using Slacker.Infrastructure;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Fake code change to test the pipeline

builder.AddApi();
builder.AddInfrastructure();


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
