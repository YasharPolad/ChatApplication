using Slacker.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Interfaces;
public interface IIdentityService
{
    Task<RegisterResponse> RegisterUserAsync(string email, string password, string phoneNumber);
    
}
