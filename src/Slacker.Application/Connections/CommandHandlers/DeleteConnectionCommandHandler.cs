using MediatR;
using Microsoft.EntityFrameworkCore;
using Slacker.Application.Connections.Commands;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.CommandHandlers;
internal class DeleteConnectionCommandHandler : IRequestHandler<DeleteConnectionCommand, BaseMediatrResult>
{
    private readonly IConnectionRepository _connectionRepository;

    public DeleteConnectionCommandHandler(IConnectionRepository connectionRepository)
    {
        _connectionRepository = connectionRepository;
    }

    public async Task<BaseMediatrResult> Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseMediatrResult();

        var connectionToDelete = await _connectionRepository.GetAsync(c => c.Id == request.Id);
        if(connectionToDelete is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("The connection to delete wasn't found!");
            return result;
        }

        await _connectionRepository.DeleteAsync(connectionToDelete);
        result.IsSuccess = true;
        return result;
    }
}
