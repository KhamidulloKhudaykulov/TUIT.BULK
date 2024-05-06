using Bulk.Domain.Commons;

namespace Bulk.Domain.Entities;

public class Actor : Auditable
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; }
    public string Role { get; set; }
}
