using Bulk.Domain.Commons;

namespace Bulk.Domain.Entities;

public class Category : Auditable
{
    public string Name { get; set; }
    public long ParentId { get; set; }
}
