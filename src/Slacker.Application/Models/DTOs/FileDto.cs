using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Models.DTOs;
public class FileDto
{
    public FileStream FileStream { get; set; }
    public string ContentType { get; set; }
}
