using Bulk.Domain.Entities;
using Bulk.Model.Categories;

namespace Bulk.Model.Products;

public class ProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public CategoryViewModel Category { get; set; }
    public int Count { get; set; }
}
