using MediatR;
using Slacker.Application.Connections.Commands;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Models;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.CommandHandlers;
internal class CreateConnectionCommandHandler : IRequestHandler<CreateConnectionCommand, MediatrResult<Connection>>
{
    private readonly IConnectionRepository _repository;
    public CreateConnectionCommandHandler(IConnectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<MediatrResult<Connection>> Handle(CreateConnectionCommand request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<Connection>();
        var connection = new Connection
        {
            Name = request.Name,
            IsChannel = request.IsChannel,
            IsPrivate = request.IsPrivate  //TODO: Add the creator employee to the employees list
        };

        await _repository.CreateAsync(connection); //If there is an exception it will be caught by the global exception handler. 
        
        result.Payload = connection;
        result.IsSuccess = true;
        return result;
    }
}
