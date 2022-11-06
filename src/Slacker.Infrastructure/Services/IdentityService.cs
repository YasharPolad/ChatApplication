using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Slacker.Application.Interfaces;
using Slacker.Application.Models.User;
using Slacker.Infrastructure.ConfigOptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.Services;
public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;

    public IdentityService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
       IConfiguration configuration, IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<LoginMediatrResult> LoginUserAsync(string email, string password)
    {
        var result = new LoginMediatrResult();
        var loginUser = await _userManager.FindByEmailAsync(email);

        if(loginUser is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("Username doesn't exist");
            return result;
        } 
        
        bool passwordCheck = await _userManager.CheckPasswordAsync(loginUser, password);
        if(!passwordCheck)
        {
            result.IsSuccess = false;
            result.Errors.Add("Username or Password is incorrect");
            return result;
        }

        List<Claim> Claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
        };

        var userRoles = await _userManager.GetRolesAsync(loginUser);
        foreach (var role in userRoles)
        {
            Claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: Claims,
            expires: DateTime.Today.AddDays(7),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

        result.IsSuccess = true;
        result.Token = tokenAsString;
        result.ExpirationDate = token.ValidTo;
        return result;

    }

    public async Task<RegisterMediatrResult> RegisterUserAsync(string email, string password, string phoneNumber)
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
            return new RegisterMediatrResult
            {
                IsSuccess = true,
            };

        }

        return new RegisterMediatrResult
        {
            IsSuccess = false,
            Errors = result.Errors.Select(error => error.Description).ToList()
        };
    }
}
