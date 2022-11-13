using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Models;
public class MediatrResult<T> : BaseMediatrResult
{
    public T? Payload { get; set; }

}
