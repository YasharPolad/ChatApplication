using MediatR;
using Slacker.Application.Users.Commands;
using Slacker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program), typeof(RegisterRequestCommand));
builder.Services.AddMediatR(typeof(Program), typeof(RegisterRequestCommand));

builder.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
