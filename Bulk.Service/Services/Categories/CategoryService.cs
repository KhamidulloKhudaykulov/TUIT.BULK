using AutoMapper;
using Bulk.DataAccess.UnitOfWorks;
using Bulk.Domain.Entities;
using Bulk.Model.Categories;
using Bulk.Service.Exceptions;

namespace Bulk.Service.Services.Categories;

public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    public async ValueTask<CategoryViewModel> CreateAsync(CategoryCreateModel model)
    {
        var data = await unitOfWork.Categories.SelectAsync(c => c.Name.ToLower() == model.Name.ToLower() && !c.IsDeleted);
        if (data != null)
            throw new AlreadyExistException($"This category with name={model.Name} is already exists");

        await unitOfWork.Categories.InsertAsync(mapper.Map<Category>(model));
        await unitOfWork.SaveAsync();

        return mapper.Map<CategoryViewModel>(model);
    }

    public async ValueTask<CategoryViewModel> DeleteAsync(long id)
    {
        var data = await unitOfWork.Categories.SelectAsync(c => c.Id == id && !c.IsDeleted);
        if (data == null)
            throw new NotFoundException($"This category with ID={id}");

        await unitOfWork.Categories.DeleteAsync(data);
        await unitOfWork.SaveAsync();

        return mapper.Map<CategoryViewModel>(data);
    }

    public async ValueTask<IEnumerable<CategoryViewModel>> GetAllAsync()
    {
        var data = await unitOfWork.Categories.SelectAllAsEnumerable(c => !c.IsDeleted);
        return mapper.Map<IEnumerable<CategoryViewModel>>(data);
    }

    public async ValueTask<CategoryViewModel> GetAsync(long id)
    {
        var data = await unitOfWork.Categories.SelectAsync(c => c.Id == id && !c.IsDeleted);
        if (data == null)
            throw new NotFoundException($"This category with ID={id}");

        return mapper.Map<CategoryViewModel>(data);
    }

    public async ValueTask<CategoryViewModel> UpdateAsync(long id, CategoryViewModel model)
    {
        var data = await unitOfWork.Categories.SelectAsync(c => c.Id == id && !c.IsDeleted);
        if (data != null)
            throw new NotFoundException($"This category with ID={id}");

        await unitOfWork.Categories.UpdateAsync(data);
        await unitOfWork.SaveAsync();
        return mapper.Map<CategoryViewModel>(data);
    }
}
