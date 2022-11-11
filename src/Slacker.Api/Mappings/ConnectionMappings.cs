using AutoMapper;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.Connection;
using Slacker.Application.Connections.Commands;
using Slacker.Application.Models;
using Slacker.Domain.Entities;

namespace Slacker.Api.Mappings;

public class ConnectionMappings : Profile
{
	public ConnectionMappings()
	{
		CreateMap<CreateConnection, CreateConnectionCommand>();
		CreateMap<MediatrResult<Connection>, ErrorResponse>();
	}
}
