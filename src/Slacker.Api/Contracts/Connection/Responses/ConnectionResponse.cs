namespace Slacker.Api.Contracts.Connection.Responses;

public class ConnectionResponse
{
    public string Name { get; set; }
    public bool IsChannel { get; set; }
    public bool IsPrivate { get; set; }
    /*public ICollection<string> EmployeeIds { get; set; }*/ //TODO: Employee names in the future. Learn how to map this
}
