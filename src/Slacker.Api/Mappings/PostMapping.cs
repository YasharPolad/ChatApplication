using AutoMapper;
using Slacker.Api.Contracts.Posts.Request;
using Slacker.Api.Contracts.Posts.Response;
using Slacker.Application.Posts.Commands;
using Slacker.Domain.Entities;

namespace Slacker.Api.Mappings;

public class PostMapping : Profile
{
	public PostMapping()
	{
		CreateMap<CreatePost, CreatePostCommand>();
		CreateMap<Post, CreatePostResponse>();
	}
}
