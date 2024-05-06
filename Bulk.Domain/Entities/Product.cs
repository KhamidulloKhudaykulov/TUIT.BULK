using Bulk.Domain.Commons;

namespace Bulk.Domain.Entities;

public class Product : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public int Count { get; set; }
}
