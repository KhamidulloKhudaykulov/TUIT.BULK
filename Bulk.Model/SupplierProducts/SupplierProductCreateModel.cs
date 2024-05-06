using Bulk.Domain.Entities;

namespace Bulk.Model.SupplierProducts;

public class SupplierProductCreateModel
{
    public List<Supplier> Suppliers { get; set; }
    public List<Product> Products { get; set; }
}
