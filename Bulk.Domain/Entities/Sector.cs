using Bulk.Domain.Commons;

namespace Bulk.Domain.Entities;

public class Sector : Auditable
{
    public string Block { get; set; }
    public int Number { get; set; }
    public IList<Product> Products { get; set; }
    public int Count { get; set; }
}
