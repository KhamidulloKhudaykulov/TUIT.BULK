using AutoMapper;
using Bulk.DataAccess.UnitOfWorks;
using Bulk.Domain.Entities;
using Bulk.Model.Categories;
using Bulk.Model.Products;
using Bulk.Service.Exceptions;

namespace Bulk.Service.Services.Products;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    public async ValueTask<ProductViewModel> CreateAsync(ProductCreateModel model)
    {
        var product = await unitOfWork.Products.SelectAsync(
            expression: p => p.Name.ToLower() == model.Name.ToLower() && !p.IsDeleted,
            includes: ["Category"]);

        var createdProduct = mapper.Map<Product>(model);
        if (product != null)
        {
            product.Count += createdProduct.Count;
            await unitOfWork.Products.UpdateAsync(product);
            await unitOfWork.SaveAsync();
        }
        else
        {
            await unitOfWork.Products.InsertAsync(createdProduct);
            await unitOfWork.SaveAsync();
        }

        return mapper.Map<ProductViewModel>(createdProduct);
    }

    public async ValueTask<ProductViewModel> DeleteAsync(long id)
    {
        var product = await unitOfWork.Products.SelectAsync(p => p.Id == id && !p.IsDeleted);
        if (product == null)
            throw new NotFoundException($"This product with ID={id} is not found");

        await unitOfWork.Products.DeleteAsync(product);
        await unitOfWork.SaveAsync();

        return mapper.Map<ProductViewModel>(product);
    }

    public async ValueTask<IEnumerable<ProductViewModel>> GetAllAsync()
    {
        var products = await unitOfWork.Products.SelectAllAsEnumerable(
            expression: p => !p.IsDeleted,
            includes: ["Category"]);

        products.Select(p => mapper.Map<CategoryViewModel>(p.Category));
        return mapper.Map<IEnumerable<ProductViewModel>>(products);
    }

    public async ValueTask<ProductViewModel> GetAsync(long id)
    {
        var product = await unitOfWork.Products.SelectAsync(p => p.Id == id && !p.IsDeleted);
        if (product == null)
            throw new NotFoundException($"Product with ID={id} is not found");

        return mapper.Map<ProductViewModel>(product);
    }

    public async ValueTask<ProductViewModel> UpdateAsync(long id, ProductUpdateModel model)
    {
        var product = await unitOfWork.Products.SelectAsync(p => p.Id == id && !p.IsDeleted);
        if (product == null)
            throw new NotFoundException($"Product with ID={id} is not found");

        product.Name = model.Name;
        product.Description = model.Description;
        product.CategoryId = model.CategoryId;
        product.Count = model.Count;
        product.UpdatedAt = DateTime.Now;

        await unitOfWork.Products.UpdateAsync(product);
        await unitOfWork.SaveAsync();

        return mapper.Map<ProductViewModel>(product);
    }
}
