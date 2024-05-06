using Bulk.Model.Products;

namespace Bulk.Service.Services.Products;

public interface IProductService
{
    ValueTask<ProductViewModel> CreateAsync(ProductCreateModel model);
    ValueTask<ProductViewModel> UpdateAsync(long id, ProductUpdateModel model);
    ValueTask<ProductViewModel> DeleteAsync(long id);
    ValueTask<ProductViewModel> GetAsync(long id);
    ValueTask<IEnumerable<ProductViewModel>> GetAllAsync();
}
