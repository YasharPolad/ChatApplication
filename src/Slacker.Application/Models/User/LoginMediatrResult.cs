using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Models.User;
public class LoginMediatrResult
{
    public string? Token { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}
