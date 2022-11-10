using Slacker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Domain.Entities;
public class Reaction : Entity
{
    public int PostId { get; set; }
    public int Emoji { get; set; }

    public Post Post { get; set; }
}
