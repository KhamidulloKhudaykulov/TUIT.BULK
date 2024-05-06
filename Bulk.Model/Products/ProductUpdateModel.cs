namespace Bulk.Model.Products;

public class ProductUpdateModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public int Count { get; set; }
}
