using Slacker.Application.Models.DTOs;

namespace Slacker.Api.Contracts.Connection.Responses;

public class ConnectionResponse
{
    public string Name { get; set; }
    public bool IsChannel { get; set; }
    public bool IsPrivate { get; set; }
    public ICollection<EmployeeDto> Employees { get; set; }
}
