using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Models.DTOs;
public class LoginResponseDto
{
    public string? Token { get; set; }
    public DateTime? ExpirationDate { get; set; }
}
