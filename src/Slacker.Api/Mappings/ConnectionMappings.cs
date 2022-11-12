using AutoMapper;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.Connection.Requests;
using Slacker.Api.Contracts.Connection.Responses;
using Slacker.Application.Connections.Commands;
using Slacker.Application.Models;
using Slacker.Application.Models.Connection;
using Slacker.Domain.Entities;

namespace Slacker.Api.Mappings;

public class ConnectionMappings : Profile
{
	public ConnectionMappings()
	{
		CreateMap<MediatrResult<Connection>, ErrorResponse>();
		CreateMap<AddEmployeeToConnectionMediatrResult, ErrorResponse>();
		CreateMap<DeleteConnectionMediatrResult, ErrorResponse>();
		CreateMap<BaseMediatrResult, ErrorResponse>();

        CreateMap<CreateConnection, CreateConnectionCommand>();
        CreateMap<Connection, ConnectionResponse>();
		CreateMap<AddEmployeeToConnection, AddEmployeeToConnectionCommand>();
		CreateMap<UpdateConnection, UpdateConnectionCommand>();
	}
}
