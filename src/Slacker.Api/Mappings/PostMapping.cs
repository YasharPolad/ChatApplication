using AutoMapper;
using Slacker.Api.Contracts.Posts.Request;
using Slacker.Api.Contracts.Posts.Response;
using Slacker.Application.Posts.Commands;
using Slacker.Application.Posts.Queries;
using Slacker.Domain.Entities;

namespace Slacker.Api.Mappings;

public class PostMapping : Profile
{
	public PostMapping()
	{
		CreateMap<CreatePost, CreatePostCommand>();
		CreateMap<Attachment, AttachmentDto>(); //TODO: Maybe have only relative paths in attachment dto?
		CreateMap<Post, CreatePostResponse>();
		CreateMap<UpdatePost, UpdatePostCommand>();

		CreateMap<Post, GetPostResponse>()
			.ForMember(dest => dest.ReactionCount, options => options.MapFrom(src => src.Reactions.Count))
			.ForMember(dest => dest.ReplyCount, options => options.MapFrom(src => src.ChildPosts.Count));
		CreateMap<Post, GetReplyResponse>()
			.ForMember(dest => dest.ReactionCount, options => options.MapFrom(src => src.Reactions.Count));

    }
}
