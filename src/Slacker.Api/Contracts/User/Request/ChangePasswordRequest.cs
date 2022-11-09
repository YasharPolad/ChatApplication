using System.ComponentModel.DataAnnotations;

namespace Slacker.Api.Contracts.User.Request;

public class ChangePasswordRequest
{
    [Required]
    public string OldPassword { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string NewPassword { get; set; }
    [Required]
    [Compare("NewPassword", ErrorMessage = "The passwords don't match")]
    public string NewPasswordConfirmation { get; set; }
}
