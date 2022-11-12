using MediatR;
using Slacker.Application.Connections.Commands;
using Slacker.Application.Interfaces;
using Slacker.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.CommandHandlers;
internal class UpdateConnectionCommandHandler : IRequestHandler<UpdateConnectionCommand, BaseMediatrResult>
{
    private readonly ISlackerDbContext _context;
public UpdateConnectionCommandHandler(ISlackerDbContext context)
    {
        _context = context;
    }

    public async Task<BaseMediatrResult> Handle(UpdateConnectionCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseMediatrResult();
        
        var connectionToUpdate = await _context.Connections.FindAsync(request.ConnectionId);
        if(connectionToUpdate is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("Connection wasn't found! :(");
            return result;
        }
        connectionToUpdate.Name = request.Name;  //TODO: This is bullshit!
        connectionToUpdate.IsPrivate = request.IsPrivate;
        
        _context.Connections.Update(connectionToUpdate);
        await _context.SaveChangesAsync(cancellationToken);

        result.IsSuccess = true;
        return result;
    }
}
