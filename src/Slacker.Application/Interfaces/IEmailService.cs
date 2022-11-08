﻿using Slacker.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Interfaces;
public interface IEmailService
{
    Task<EmailResponse> SendMailAsync(string recipient, string heading, string body);
}
