using Slacker.Application.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Slacker.Api.Contracts.Posts.Request;

public class CreatePost
{
    public int ConnectionId { get; set; }
    [StringLength(1500)]
    public string Message { get; set; }
    public IFormFile? File { get; set; }
    public int? ParentPost { get; set; }
}
