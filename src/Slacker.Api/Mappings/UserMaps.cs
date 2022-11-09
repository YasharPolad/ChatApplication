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
        CreateMap<RegisterMediatrResult, ErrorResponse>();  //TODO: These all need to be one.
        CreateMap<LoginMediatrResult, ErrorResponse>();
		CreateMap<ForgotPasswordMediatrResult, ErrorResponse>();
		CreateMap<ConfirmEmailMediatrResult, ErrorResponse>();
		CreateMap<ResetPasswordMediatrResult, ErrorResponse>();

        CreateMap<RegisterRequest, RegisterRequestCommand>();

		CreateMap<LoginRequest, LoginCommand>();
		CreateMap<LoginMediatrResult, LoginResponse>();

		CreateMap<ResetPasswordRequest, ResetPasswordCommand>();
	}
}
