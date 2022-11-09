using MediatR;
using Microsoft.AspNetCore.Mvc;
using Slacker.Api.Filters;
using Slacker.Application.Users.Commands;
using Slacker.Infrastructure;

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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
