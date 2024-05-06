namespace Bulk.Model.Baskets;

public class BasketUpdateModel
{
    public long Id { get; set; }
    public long ActorId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
}
