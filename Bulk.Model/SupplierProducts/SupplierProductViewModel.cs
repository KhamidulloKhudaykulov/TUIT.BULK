using Bulk.Domain.Entities;

namespace Bulk.Model.SupplierProducts;

public class SupplierProductViewModel
{
    public long Id { get; set; }
    public List<Supplier> Suppliers { get; set; }
    public List<Product> Products { get; set; }
}
