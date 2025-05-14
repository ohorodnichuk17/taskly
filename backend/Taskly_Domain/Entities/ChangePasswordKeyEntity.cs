using System.ComponentModel.DataAnnotations;

namespace Taskly_Domain.Entities;

public class ChangePasswordKeyEntity : TempEntity
{
    [Key]
    public Guid Key { get; set; }
    public required string Email { get; set; }
}
