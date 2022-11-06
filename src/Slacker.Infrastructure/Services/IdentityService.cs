using Microsoft.AspNetCore.Identity;
using Slacker.Application.Interfaces;
using Slacker.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.Services;
public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterResponse> RegisterUserAsync(string email, string password, string phoneNumber)
    {
        var user = new IdentityUser
        {
            Email = email,
            UserName = email,
            PhoneNumber = phoneNumber,
        };

        IdentityResult result = await _userManager.CreateAsync(user, password);

        if(result.Succeeded)
        {
            return new RegisterResponse
            {
                IsSuccess = true,
            };

        }

        return new RegisterResponse
        {
            IsSuccess = false,
            Errors = result.Errors.Select(error => error.Description).ToList()
        };
    }
}
