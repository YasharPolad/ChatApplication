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
internal class AddEmployeeToConnectionCommandHandler : IRequestHandler<AddEmployeeToConnectionCommand, BaseMediatrResult>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AddEmployeeToConnectionCommandHandler(IConnectionRepository connectionRepository, IEmployeeRepository employeeRepository)
    {
        _connectionRepository = connectionRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<BaseMediatrResult> Handle(AddEmployeeToConnectionCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseMediatrResult();
        var connection = await _connectionRepository.GetAsync(c => c.Id == request.ConnectionId, c => c.Employees);
        if (connection == null)
        {
            result.IsSuccess = false;
            result.Errors.Add("The connection wasn't found");
            return result;
        }
        var employeesInConnection = connection.Employees.ToList();
        if(!connection.IsChannel && employeesInConnection.Count == 2)
        {
            result.IsSuccess = false;
            result.Errors.Add("Can't add a third employee to a direct message");
            return result;
        }

        var employeeToAdd = await _employeeRepository.GetAsync(e => e.Id == request.EmployeeId);
        if(employeeToAdd == null)
        {
            result.IsSuccess = false;
            result.Errors.Add("The employee wasn't found");
            return result;
        }
        else if(connection.Employees.Contains(employeeToAdd))
        {
            result.IsSuccess = false;
            result.Errors.Add($"Employee {request.EmployeeId} is already in connection {request.ConnectionId}");
            return result;
        }
        
        connection.Employees.Add(employeeToAdd);
        await _connectionRepository.UpdateAsync(connection);

        result.IsSuccess = true;
        return result;
    }
}
