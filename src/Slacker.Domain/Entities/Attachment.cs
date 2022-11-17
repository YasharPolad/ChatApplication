using Slacker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Domain.Entities;
public class Attachment : Entity
{
    public int PostId { get; set; }
    public string FilePath { get; set; }
    //public int Type { get; set; } //TODO: Enum types
    public Post Post { get; set; }
}
