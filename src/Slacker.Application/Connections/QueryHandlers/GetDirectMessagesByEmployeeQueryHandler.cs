using MediatR;
using Microsoft.EntityFrameworkCore;
using Slacker.Application.Connections.Queries;
using Slacker.Application.Interfaces;
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
    private readonly ISlackerDbContext _context;
    public GetDirectMessagesByEmployeeQueryHandler(ISlackerDbContext context)
    {
        _context = context;
    }

    public async Task<MediatrResult<List<Connection>>> Handle(GetDirectMessagesByEmployeeQuery request, CancellationToken cancellationToken)
    {
        var result = new MediatrResult<List<Connection>>();

        var employee = _context.Employees
            .Include(e => e.Connections)  //Should I instead filter here?
            .FirstOrDefault(e => e.Id == request.EmployeeId);

        if (employee is null)  //Not necessary to check this, can return empty list instead. But checking is better I think.
        {
            result.IsSuccess = false;
            result.ErrorMessages.Add("This employee doesn't exist");
            return result;
        }

        result.IsSuccess = true;
        result.Payload = employee.Connections.Where(c => c.IsChannel == false).ToList();
        return result;
    }
}
