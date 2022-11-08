using Slacker.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Interfaces;
public interface IIdentityService
{
    Task<RegisterMediatrResult> RegisterUserAsync(string email, string password, string phoneNumber);
    Task<LoginMediatrResult> LoginUserAsync(string email, string password);
    Task<ConfirmEmailMediatrResult> ConfirmEmailAsync(string token, string email);
}
