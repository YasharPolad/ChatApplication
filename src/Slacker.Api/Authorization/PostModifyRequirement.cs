using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using System.Security.Claims;

namespace Slacker.Api.Authorization;

public class PostModifyRequirement : IAuthorizationRequirement
{

}

public class PostModifyRequirementHandler : AuthorizationHandler<PostModifyRequirement>
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ISlackerDbContext _context;

    public PostModifyRequirementHandler(IHttpContextAccessor contextAccessor, 
           ISlackerDbContext context)
    {
        _contextAccessor = contextAccessor;
        _context = context;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PostModifyRequirement requirement)
    {
        if(!context.User.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            return Task.CompletedTask;

        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var postId = int.Parse(_contextAccessor.HttpContext.GetRouteValue("id").ToString());
        var post = _context.Posts.Include(p => p.Employee).FirstOrDefault(p => p.Id == postId);
        if (post.Employee.IdentityId == userId)
            context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}
