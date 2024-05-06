using AutoMapper;
using Bulk.DataAccess.UnitOfWorks;
using Bulk.Domain.Entities;
using Bulk.Model.Suppliers;
using Bulk.Service.Exceptions;

namespace Bulk.Service.Services.Suppliers;

public class SupplierService(IUnitOfWork unitOfWork, IMapper mapper) : ISupplierService
{
    public async ValueTask<SupplierViewModel> CreateAsync(SupplierCreateModel model)
    {
        var supplier = await unitOfWork.Suppliers.SelectAsync(s => s.Phone == model.Phone || s.Email == model.Email && !s.IsDeleted);
        if (supplier != null)
            throw new AlreadyExistException($"This supplier is already exists");

        var createdSupplier = mapper.Map<Supplier>(supplier);
        await unitOfWork.Suppliers.InsertAsync(createdSupplier);
        await unitOfWork.SaveAsync();

        return mapper.Map<SupplierViewModel>(createdSupplier);
    }

    public async ValueTask<SupplierViewModel> DeleteAsync(long id)
    {
        var supplier = await unitOfWork.Suppliers.SelectAsync(s => s.Id == id && !s.IsDeleted);
        if (supplier == null)
            throw new NotFoundException($"This supplier with ID={id} is not found");

        await unitOfWork.Suppliers.DeleteAsync(supplier);
        await unitOfWork.SaveAsync();

        return mapper.Map<SupplierViewModel>(supplier);
    }

    public async ValueTask<IEnumerable<SupplierViewModel>> GetAllAsync()
    {
        var suppliers = await unitOfWork.Suppliers.SelectAllAsEnumerable(
            expression: s => !s.IsDeleted);

        return mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
    }

    public async ValueTask<SupplierViewModel> GetAsync(long id)
    {
        var supplier = await unitOfWork.Suppliers.SelectAsync(s => s.Id == id && !s.IsDeleted);
        if (supplier == null)
            throw new NotFoundException($"This supplier with ID={id} is not found");

        return mapper.Map<SupplierViewModel>(supplier);
    }

    public async ValueTask<SupplierViewModel> UpdateAsync(long id, SupplierCreateModel model)
    {
        var supplier = await unitOfWork.Suppliers.SelectAsync(s => s.Id == id && !s.IsDeleted);
        if (supplier == null)
            throw new NotFoundException($"This supplier with ID={id} is not found");

        supplier.Phone = model.Phone;
        supplier.Name = model.Name;
        supplier.Email = model.Email;
        await unitOfWork.Suppliers.UpdateAsync(supplier);
        await unitOfWork.SaveAsync();

        return mapper.Map<SupplierViewModel>(supplier);
    }
}
