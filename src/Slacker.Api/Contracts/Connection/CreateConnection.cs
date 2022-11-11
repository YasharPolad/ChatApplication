using System.ComponentModel.DataAnnotations;

namespace Slacker.Api.Contracts.Connection;

public class CreateConnection
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    public bool IsChannel { get; set; }
    [Required]
    public bool IsPrivate { get; set; }
}
