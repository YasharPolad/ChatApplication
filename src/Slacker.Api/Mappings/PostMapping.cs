using AutoMapper;
using Slacker.Api.Contracts.Posts.Request;
using Slacker.Application.Posts.Commands;

namespace Slacker.Api.Mappings;

public class PostMapping : Profile
{
	public PostMapping()
	{
		CreateMap<CreatePost, CreatePostCommand>();
	}
}
