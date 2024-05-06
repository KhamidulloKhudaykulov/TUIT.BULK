using Bulk.Model.Categories;

namespace Bulk.Service.Services.Categories;

public interface ICategoryService
{
    ValueTask<CategoryViewModel> CreateAsync(CategoryCreateModel model);
    ValueTask<CategoryViewModel> UpdateAsync(long id, CategoryViewModel model);
    ValueTask<CategoryViewModel> DeleteAsync(long id);
    ValueTask<CategoryViewModel> GetAsync(long id);
    ValueTask<IEnumerable<CategoryViewModel>> GetAllAsync();
}
