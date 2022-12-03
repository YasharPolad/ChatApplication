using MediatR;
using Microsoft.EntityFrameworkCore;
using Slacker.Application.Connections.Queries;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Models;
using Slacker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Connections.QueryHandlers;
internal class GetDirectMessagesByEmployeeQueryHandler : IRequestHandler<GetDirectMessagesByEmployeeQuery, MediatrResult<List<Connection>>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IConnectionRepository _connectionRepository;

    public GetDirectMessagesByEmployeeQueryHandler(IEmployeeRepository employeeRepository, IConnectionRepository connectionRepository)
    {
        _employeeRepository = employeeRepository;
        _connectionRepository = connectionRepository;
    }

    public async Task<MediatrResult<List<Connection>>> Handle(GetDirectMessagesByEmployeeQuery request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<List<Connection>>();


        var employee = await _employeeRepository.GetAsync(e => e.Id == request.EmployeeId, e => e.Connections);

        if (employee is null)  //Not necessary to check this, can return empty list instead. But checking is better I think.
        {
            result.IsSuccess = false;
            result.Errors.Add("This employee doesn't exist");
            return result;
        }

        //var channels = employee.Connections.Where(c => c.IsChannel == false).ToList();
        var channels = await _connectionRepository
            .GetAllAsync(c => c.Employees.Any(e => e.Id == employee.Id) && c.IsChannel == false, c => c.Employees);


        result.IsSuccess = true;
        result.Payload = channels;
        return result;
    }
}
