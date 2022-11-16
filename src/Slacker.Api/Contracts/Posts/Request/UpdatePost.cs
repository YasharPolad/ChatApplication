using System.ComponentModel.DataAnnotations;

namespace Slacker.Api.Contracts.Posts.Request;

public class UpdatePost
{
    [StringLength(1500)]
    public string updatedMessage { get; set; }
}
