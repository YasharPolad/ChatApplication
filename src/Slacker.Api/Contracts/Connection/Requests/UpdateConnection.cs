using System.ComponentModel.DataAnnotations;

namespace Slacker.Api.Contracts.Connection.Requests;

public class UpdateConnection
{
    public bool IsPrivate { get; set; } //You can not update IsConnection, because you can't turn a dm into a channel or vice versa
    [StringLength(50)]
    public string Name { get; set; }
}
