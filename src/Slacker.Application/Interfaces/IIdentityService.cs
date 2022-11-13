using Slacker.Application.Models;
using Slacker.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Interfaces;
public interface IIdentityService //TODO: This needs to be rewritten so that some of the logic is in the command handler. Currently all of it is inside the identityservice. Have methods that do smaller jobs, like get a user(id), find a user, confirm an email etc.
{
    Task<BaseMediatrResult> RegisterUserAsync(string email, string password, string phoneNumber);
    Task<MediatrResult<LoginResponseDto>> LoginUserAsync(string email, string password);
    Task<BaseMediatrResult> ConfirmEmailAsync(string token, string email);
    Task<BaseMediatrResult> ResetPasswordAsync(string newPassword, string email, string token);
    Task<BaseMediatrResult> ForgotPasswordAsync(string email);
    Task<BaseMediatrResult> ChangePassword(string oldPassword, string newPassword, string email);
}
