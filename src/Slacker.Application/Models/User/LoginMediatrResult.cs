using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Models.User;
public class LoginMediatrResult : BaseMediatrResult
{
    public string? Token { get; set; }
    public DateTime? ExpirationDate { get; set; }

}
