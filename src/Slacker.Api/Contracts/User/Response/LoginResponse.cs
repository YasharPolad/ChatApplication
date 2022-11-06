namespace Slacker.Api.Contracts.User.Response;

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}
