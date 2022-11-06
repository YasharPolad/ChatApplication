using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Models.User;
public class RegisterMediatrResult
{
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}
