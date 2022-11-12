using MediatR;
using Microsoft.EntityFrameworkCore;
using Slacker.Application.Connections.Commands;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Models.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.CommandHandlers;
internal class AddEmployeeToConnectionCommandHandler : IRequestHandler<AddEmployeeToConnectionCommand, AddEmployeeToConnectionMediatrResult>
{
    private readonly ISlackerDbContext _context;

    public AddEmployeeToConnectionCommandHandler(ISlackerDbContext context)
    {
        _context = context;
    }

    public async Task<AddEmployeeToConnectionMediatrResult> Handle(AddEmployeeToConnectionCommand request, CancellationToken cancellationToken)
    {
        var result = new AddEmployeeToConnectionMediatrResult();
        var connection = _context.Connections                   //Is there a way to use Include with repositories?
                            .Include(c => c.Employees)
                            .FirstOrDefault(c => c.Id == request.ConnectionId);
        if (connection == null)
        {
            result.IsSuccess = false;
            result.Errors.Add("The connection wasn't found");
            return result;
        }
        var employees = connection.Employees.ToList();
        if(!connection.IsChannel && employees.Count == 2)
        {
            result.IsSuccess = false;
            result.Errors.Add("Can't add a third employee to a direct message");
            return result;
        }
        
        var employeeToAdd = _context.Employees.Find(request.EmployeeId);
        if(employeeToAdd == null)
        {
            result.IsSuccess = false;
            result.Errors.Add("The employee wasn't found");
            return result;
        }
        
        connection.Employees.Add(employeeToAdd);
        await _context.SaveChangesAsync(cancellationToken);

        result.IsSuccess = true;
        return result;
    }
}
