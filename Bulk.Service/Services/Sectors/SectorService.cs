using AutoMapper;
using Bulk.DataAccess.UnitOfWorks;
using Bulk.Domain.Entities;
using Bulk.Model.Sectors;
using Bulk.Service.Exceptions;
using System.Xml;

namespace Bulk.Service.Services.Sectors;

public class SectorService(IUnitOfWork unitOfWork, IMapper mapper) : ISectorService
{
    public async ValueTask<SectorViewModel> CreateAsync(SectorCreateModel model)
    {
        var sector = await unitOfWork.Sectors.SelectAsync(s => s.Block == model.Block && s.Number == model.Number && !s.IsDeleted);
        if (sector != null)
            throw new AlreadyExistException($"This sector is already exists");

        var createdModel = mapper.Map<Sector>(model);
        await unitOfWork.Sectors.InsertAsync(createdModel);
        await unitOfWork.SaveAsync();

        return mapper.Map<SectorViewModel>(createdModel);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var all = await unitOfWork.Sectors.SelectAllAsEnumerable(includes: ["Products"]);
        var sector = all.FirstOrDefault(s => s.Id == id);
        if (sector == null)
            throw new NotFoundException($"This sector with ID={id} is not found");

        try
        {
            await unitOfWork.Sectors.DeleteAsync(sector);
            await unitOfWork.SaveAsync();
        }
        catch(Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }

        return true;
    }

    public async ValueTask<IEnumerable<SectorViewModel>> GetAllAsync()
    {
        var sectors = await unitOfWork.Sectors.SelectAllAsEnumerable(includes: ["Products"]);
        return mapper.Map<IEnumerable<SectorViewModel>>(sectors);
    }

    public async ValueTask<SectorViewModel> GetAsync(long id)
    {
        var sector = await unitOfWork.Sectors.SelectAsync(s => s.Id == id);
        if (sector == null)
            throw new NotFoundException($"This sector with ID={id} is not found");

        return mapper.Map<SectorViewModel>(sector);
    }

    public async ValueTask<bool> InsertProduct(long id, long productId, int count)
    {
        var sector = await unitOfWork.Sectors.SelectAsync(expression: s => s.Id == id, includes: ["Products"]);
        if (sector == null)
            throw new NotFoundException($"This sector with ID={id} is not found");

        var product = await unitOfWork.Products.SelectAsync(p => p.Id == productId, ["Category"]);
        if (product == null)
            throw new NotFoundException($"This product is not found");

        if (product.Count < count)
            throw new ArgumentIsNotValidException("The count more than product count");

        var checkSectors = await unitOfWork.Sectors.SelectAllAsEnumerable(includes: ["Products"]);
        var existProductsInSectors = checkSectors.Where(p => p.Id == productId);

        var totalCount = 0;
        if(existProductsInSectors != null)
            foreach (var item in existProductsInSectors)
                totalCount += item.Count;

        if (product.Count - totalCount < count)
            throw new ArgumentIsNotValidException($"In Inventory has not so much products");


        var sectorProduct = new Product()
        {
            Id = productId,
            CategoryId = product.CategoryId,
            Category = product.Category,
            Count = count,
            CreatedAt = product.CreatedAt,
            Name = product.Name,
            IsDeleted = false,
        };

        try
        {
            var existProduct = sector.Products.ToList().FirstOrDefault(product => product.Id == productId);
            if (existProduct == null)
            {
                sector.Products.Add(sectorProduct);
                if (sector.Products.Count < 1)
                    sector.Count = 1;
                else
                    sector.Count += 1;
            }
            else
                existProduct.Count += count;
        }
        catch
        {
            sector.Products = new List<Product>();
            sector.Products.Add(sectorProduct);
            if (sector.Products.Count < 1)
                sector.Count = 1;
            else
                sector.Count += 1;
        }

        await unitOfWork.Sectors.UpdateAsync(sector);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<bool> RemoveProduct(long id, long productId, int count)
    {
        var sector = await unitOfWork.Sectors.SelectAsync(s => s.Id == id);
        if (sector == null)
            throw new NotFoundException($"This sector with ID={id} is not found");

        var existProduct = sector.Products.FirstOrDefault(p => p.Id == productId);
        if (existProduct == null)
            throw new NotFoundException($"This product has not in this sector");

        if(existProduct.Count < count)
            throw new ArgumentIsNotValidException("The count more than product count");

        existProduct.Count -= count;
        await unitOfWork.Sectors.UpdateAsync(sector);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<SectorViewModel> UpdateAsync(long id, SectorUpdateModel model)
    {
        var sector = await unitOfWork.Sectors.SelectAsync(s => s.Id == id);
        if (sector == null)
            throw new NotFoundException($"This sector with ID={id} is not found");

        sector.Block = model.Block;
        sector.Number = model.Number;
        sector.UpdatedAt = DateTime.UtcNow;
        await unitOfWork.Sectors.UpdateAsync(sector);
        await unitOfWork.SaveAsync();

        return mapper.Map<SectorViewModel>(sector);
    }
}
