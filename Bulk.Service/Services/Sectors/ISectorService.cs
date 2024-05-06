using Bulk.Model.Sectors;

namespace Bulk.Service.Services.Sectors;

public interface ISectorService
{
    ValueTask<SectorViewModel> CreateAsync(SectorCreateModel model);
    ValueTask<SectorViewModel> UpdateAsync(long id, SectorUpdateModel model);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<SectorViewModel> GetAsync(long id);
    ValueTask<IEnumerable<SectorViewModel>> GetAllAsync();
    ValueTask<bool> InsertProduct(long id, long productId, int count);
    ValueTask<bool> RemoveProduct(long id, long productId, int count);
}
