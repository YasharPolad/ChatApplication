namespace Slacker.Api.Contracts;

public class ErrorResponse
{
    public List<string> Errors { get; set; } = new List<string>();
}
