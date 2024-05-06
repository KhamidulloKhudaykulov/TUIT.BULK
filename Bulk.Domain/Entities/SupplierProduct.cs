using Bulk.Domain.Commons;

namespace Bulk.Domain.Entities;

public class SupplierProduct : Auditable
{
    public IList<Supplier> Suppliers { get; set; }
    public IList<Product> Products { get; set; }
}
