using Slacker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Domain.Entities;
public class Employee : Entity
{
    public Employee()
    {
        Connections = new List<Connection>();
        Posts = new List<Post>();
    }
    public string FullName { get; set; }
    public string SigalRId { get; set; }
    public string IdentityId { get; set; }
    public ICollection<Connection> Connections { get; set; }

    public ICollection<Post> Posts { get; set; }
}
