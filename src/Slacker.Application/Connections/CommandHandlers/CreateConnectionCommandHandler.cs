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
public class CreateConnectionCommandHandler : IRequestHandler<CreateConnectionCommand, MediatrResult<Connection>>
{
    private readonly IConnectionRepository _repository;
    private readonly IEmployeeRepository _employeeRepository;
    public CreateConnectionCommandHandler(IConnectionRepository repository, IEmployeeRepository employeeRepository)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
    }

    public async Task<MediatrResult<Connection>> Handle(CreateConnectionCommand request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<Connection>();
        var connection = new Connection
        {
            Name = request.Name,
            IsChannel = request.IsChannel,
            IsPrivate = request.IsPrivate  
        };

        var currentEmployee = await _employeeRepository.GetAsync(e => e.IdentityId == request.CreatingUserId);
        connection.Employees.Add(currentEmployee); 

        await _repository.CreateAsync(connection); //If there is an exception it will be caught by the global exception handler. 
        
        result.Payload = connection;
        result.IsSuccess = true;
        return result;
    }
}
