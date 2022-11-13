using AutoMapper;
using Slacker.Api.Contracts;
using Slacker.Api.Contracts.User.Request;
using Slacker.Api.Contracts.User.Response;
using Slacker.Application.Models.DTOs;
using Slacker.Application.Users.Commands;

namespace Slacker.Api.Mappings;

public class UserMaps : Profile
{
	public UserMaps()
	{
        //CreateMap<LoginMediatrResult, ErrorResponse>();

        CreateMap<RegisterRequest, RegisterRequestCommand>();

		CreateMap<LoginRequest, LoginCommand>();
		CreateMap<LoginResponseDto, LoginResponse>();

		CreateMap<ResetPasswordRequest, ResetPasswordCommand>();

		CreateMap<ChangePasswordRequest, ChangePasswordCommand>();
	}
}
