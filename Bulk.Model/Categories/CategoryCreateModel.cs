namespace Bulk.Model.Categories;

public class CategoryCreateModel
{
    public string Name { get; set; }
    public long ParentId { get; set; } = 0;
}
