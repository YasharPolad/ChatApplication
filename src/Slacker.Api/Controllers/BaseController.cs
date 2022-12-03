using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slacker.Api.Contracts;

namespace Slacker.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;
    private readonly ILogger _logger;

    public BaseController(IMapper mapper, IMediator mediator, ILogger logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    public IActionResult ErrorResponseHandler(ErrorResponse response)
    {
        response.Errors.ForEach(e => _logger.LogWarning(e));
        return BadRequest(response);
    }
}
