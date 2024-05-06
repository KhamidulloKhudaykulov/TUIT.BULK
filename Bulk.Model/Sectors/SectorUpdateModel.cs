using Bulk.Domain.Entities;

namespace Bulk.Model.Sectors;

public class SectorUpdateModel
{
    public long Id { get; set; }
    public string Block { get; set; }
    public int Number { get; set; }
    public int Count { get; set; }
}
