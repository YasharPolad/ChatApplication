using AutoMapper;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.User.Request;
using Slacker.Application.Models.User;
using Slacker.Application.Users.Commands;

namespace Slacker.Api.Mappings;

public class UserMaps : Profile
{
	public UserMaps()
	{
		CreateMap<RegisterRequest, RegisterRequestCommand>();
		CreateMap<RegisterResponse, ErrorResponse>();
	}
}
