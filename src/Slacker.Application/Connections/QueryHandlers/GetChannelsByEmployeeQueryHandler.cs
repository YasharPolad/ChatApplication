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
internal class GetChannelsByEmployeeQueryHandler : IRequestHandler<GetChannelsByEmployeeQuery, MediatrResult<List<Connection>>>
{
    private readonly IEmployeeRepository _employeeRepository;
    public GetChannelsByEmployeeQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<MediatrResult<List<Connection>>> Handle(GetChannelsByEmployeeQuery request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<List<Connection>>();


        var employee = await _employeeRepository.GetAsync(e => e.Id == request.EmployeeId, e => e.Connections);
        
        if(employee is null)  //Not necessary to check this, can return empty list instead. But checking is better I think.
        {
            result.IsSuccess = false;
            result.Errors.Add("This employee doesn't exist");
            return result;
        }

        var channels = employee.Connections.Where(c => c.IsChannel == true).ToList();


        result.IsSuccess = true;
        result.Payload = channels;
        return result;
    }
}
