namespace Slacker.Api.Contracts.Posts.Response;

public class GetReplyResponse
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ReactionCount { get; set; }
}
