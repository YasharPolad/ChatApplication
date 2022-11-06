using AutoMapper;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.User.Request;
using Slacker.Api.Contracts.User.Response;
using Slacker.Application.Models.User;
using Slacker.Application.Users.Commands;

namespace Slacker.Api.Mappings;

public class UserMaps : Profile
{
	public UserMaps()
	{
		CreateMap<RegisterRequest, RegisterRequestCommand>();
		CreateMap<RegisterMediatrResult, ErrorResponse>();

		CreateMap<LoginRequest, LoginCommand>();
		CreateMap<LoginMediatrResult, LoginResponse>();
		CreateMap<LoginMediatrResult, ErrorResponse>();
	}
}
