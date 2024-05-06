using Bulk.Domain.Commons;

namespace Bulk.Domain.Entities;

public class Supplier : Auditable
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}
