using System.ComponentModel.DataAnnotations;

namespace Slacker.Api.Contracts.User.Request;

public class ResetPasswordRequest
{
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string NewPassword { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 6)]
    [Compare("NewPassword", ErrorMessage = "Passwords don't match")]
    public string NewPasswordConfirmation { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
