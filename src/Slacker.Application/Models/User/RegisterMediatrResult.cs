using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Models.User;
public class RegisterMediatrResult : MediatrResult
{
    public string EmailConfirmationToken { get; set; }
    public string UserEmail { get; set; }

}
