using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Slacker.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;
    protected readonly ILogger _logger;

    public BaseController(IMapper mapper, IMediator mediator, ILogger logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }
}
