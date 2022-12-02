using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Domain.Entities;
using System.Security.Claims;
using System.Text;

namespace Slacker.Api.Authorization;

public class PostCreateRequirement : IAuthorizationRequirement
{
}

public class PostCreateRequirementHandler : AuthorizationHandler<PostCreateRequirement>
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IEmployeeRepository _employeeRepository;

    public PostCreateRequirementHandler(IHttpContextAccessor contextAccessor, IEmployeeRepository employeeRepository)
    {
        _contextAccessor = contextAccessor;
        _employeeRepository = employeeRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PostCreateRequirement requirement)
    {
        if (!context.User.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            await Task.CompletedTask;

        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var connectionId = _contextAccessor.HttpContext.Request.Form["ConnectionId"].FirstOrDefault();

        Employee employee = await _employeeRepository.GetAsync(e => e.IdentityId == userId, e => e.Connections);
        if (employee.Connections.Any(c => c.Id == int.Parse(connectionId)))
            context.Succeed(requirement);
        
        await Task.CompletedTask;
        
    }
}
