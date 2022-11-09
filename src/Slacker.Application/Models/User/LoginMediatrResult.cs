using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Models.User;
public class LoginMediatrResult : MediatrResult
{
    public string? Token { get; set; }
    public DateTime? ExpirationDate { get; set; }

}
