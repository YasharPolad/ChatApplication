using Slacker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Domain.Entities;
public class Connection : Entity
{
    public string Name { get; set; }
    public bool IsChannel { get; set; }
    public bool IsPrivate { get; set; }
    public ICollection<Employee> Employees { get; set; }

    public ICollection<Post> Posts { get; set; }
}
