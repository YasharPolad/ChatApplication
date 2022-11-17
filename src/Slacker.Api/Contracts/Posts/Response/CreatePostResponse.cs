namespace Slacker.Api.Contracts.Posts.Response;

public class CreatePostResponse  
{
    public int Id { get; set; }
    public List<AttachmentDto> Attachments { get; set; }
}
