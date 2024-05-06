namespace Bulk.Model.Baskets;

public class BasketCreateModel
{
    public long ActorId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
}
