using System.ComponentModel.DataAnnotations;

namespace Slacker.Api.Contracts.User.Request;

public class LoginRequest
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }
}
