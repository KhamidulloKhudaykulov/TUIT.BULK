using Bulk.Model.Suppliers;

namespace Bulk.Service.Services.Suppliers;

public interface ISupplierService
{
    ValueTask<SupplierViewModel> CreateAsync(SupplierCreateModel model);
    ValueTask<SupplierViewModel> UpdateAsync(long id, SupplierCreateModel model);
    ValueTask<SupplierViewModel> DeleteAsync(long id);
    ValueTask<SupplierViewModel> GetAsync(long id);
    ValueTask<IEnumerable<SupplierViewModel>> GetAllAsync();

}
