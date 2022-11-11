using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Slacker.Application.Interfaces;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Application.Models.User;
using Slacker.Domain.Entities;
using Slacker.Infrastructure.ConfigOptions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Slacker.Infrastructure.Services;
public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IEmailService _emailService;
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmployeeRepository _employeeRepository;

    public IdentityService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
       IOptions<JwtSettings> jwtOptions, IEmailService emailService, LinkGenerator linkGenerator,
       IHttpContextAccessor httpContextAccessor, IEmployeeRepository employeeRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtOptions.Value;
        _emailService = emailService;
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
        _employeeRepository = employeeRepository;
    }



    public async Task<ChangePasswordMediatrResult> ChangePassword(string oldPassword, string newPassword, string email)
    {
        var response = new ChangePasswordMediatrResult();
        var user = await _userManager.FindByEmailAsync(email); //im not going to validate this because the email comes from an authorized user
        var changePassword = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!changePassword.Succeeded)
        {
            response.IsSuccess = false;
            response.Errors.Add("Password change failed");
            return response;
        }

        response.IsSuccess = true;
        return response;

    }

    public async Task<ConfirmEmailMediatrResult> ConfirmEmailAsync(string token, string email)
    {
        var response = new ConfirmEmailMediatrResult();
        var user = await _userManager.FindByEmailAsync(email);

        if(user is null)
        {
            response.IsSuccess = false;
            response.Errors.Add("User with this email doesn't exist");
            return response;
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if(!result.Succeeded)
        {
            response.IsSuccess = false;
            response.Errors.Add("The token or email is wrong");
            return response;
        }
        
        response.IsSuccess = true;
        await _employeeRepository.CreateAsync(new Employee { IdentityId = user.Id }); //TODO: Create Employee automatically. But there must be a better way
        return response;

    }

    public async Task<ForgotPasswordMediatrResult> ForgotPasswordAsync(string email)
    {
        var result = new ForgotPasswordMediatrResult();
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("User by this email doesn't exist");
            return result;
        }

        var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var confirmationLink = _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext, "ResetPassword", "Auth", new {passwordResetToken, email});
        var response = await _emailService.SendMailAsync(email, "Password Reset", $"Click this link to reset your password: {confirmationLink}");

        if(response.StatusCode != "Accepted")
        {
            result.IsSuccess = false;
            result.Errors.Add(response.Body);
            return result;
        }

        result.IsSuccess = true;
        return result;
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

        bool isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(loginUser);
        if(!isEmailConfirmed)
        {
            result.IsSuccess = false;
            result.Errors.Add("You need to confirm your email address before signing in");
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
        var registerResult = new RegisterMediatrResult();
        var user = new IdentityUser
        {
            Email = email,
            UserName = email.Split("@")[0],
            PhoneNumber = phoneNumber,
        };

        IdentityResult result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {

            registerResult.IsSuccess = false;
            registerResult.Errors = result.Errors.Select(error => error.Description).ToList();
            return registerResult;

        }

        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "Default");

        if (!roleResult.Succeeded)
        {
            registerResult.IsSuccess = false;
            registerResult.Errors = result.Errors.Select(error => error.Description).ToList();
            return registerResult;
        }

        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext,"ConfirmEmail", "Auth", new { token, email });
        var response = await _emailService.SendMailAsync(email, "Email Confirmation", $"Click this link to confirm your email: {confirmationLink}");

        
        registerResult.IsSuccess = true;
        return registerResult;
        
    }

    public async Task<ResetPasswordMediatrResult> ResetPasswordAsync(string newPassword, string email, string token)
    {
        var result = new ResetPasswordMediatrResult();
        var user = await _userManager.FindByEmailAsync(email);
        var resetPassword = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if(!resetPassword.Succeeded)
        {
            result.IsSuccess = false;
            result.Errors.Add("Couln't change the password");
            return result;
        }

        result.IsSuccess = true;
        return result;
    }
}
