using Bulk.Domain.Commons;

namespace Bulk.Domain.Entities;

public class Basket : Auditable
{
    public long ActorId { get; set; }
    public Actor Actor { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public int Count { get; set; }
}
