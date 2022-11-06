using System.ComponentModel.DataAnnotations;

namespace Slacker.Api.Contracts.User.Request;

public class RegisterRequest
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string PasswordConfirm { get; set; }
    public string? PhoneNumber { get; set; }
}
