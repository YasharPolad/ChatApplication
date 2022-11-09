using Slacker.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Interfaces;
public interface IIdentityService //TODO: This needs to be rewritten so that some of the logic is in the command handler. Currently all of it is inside the identityservice. Have methods that do smaller jobs, like get a user(id), find a user, confirm an email etc.
{
    Task<RegisterMediatrResult> RegisterUserAsync(string email, string password, string phoneNumber);
    Task<LoginMediatrResult> LoginUserAsync(string email, string password);
    Task<ConfirmEmailMediatrResult> ConfirmEmailAsync(string token, string email);
    Task<ResetPasswordMediatrResult> ResetPasswordAsync(string newPassword, string email, string token);
    Task<ForgotPasswordMediatrResult> ForgotPasswordAsync(string email);
}
