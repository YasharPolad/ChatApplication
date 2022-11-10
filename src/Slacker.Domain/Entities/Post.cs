using Slacker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Domain.Entities;
public class Post : Entity
{
    public int ConnectionId { get; set; }
    public int EmployeeId { get; set; }
    public string Message { get; set; }
    public int ParentPostId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsEdited { get; set; }

    public Connection Connection { get; set; }
    public Employee Employee { get; set; }
    public Post ParentPost { get; set; }
    public ICollection<Post> ChildPosts { get; set; }
    public ICollection<Attachment> Attachments { get; set; }
    public ICollection<Reaction> Reactions { get; set; }

}
