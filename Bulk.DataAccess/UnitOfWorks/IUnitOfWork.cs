using Bulk.DataAccess.Repositrories;
using Bulk.Domain.Entities;

namespace Bulk.DataAccess.UnitOfWorks;

public interface IUnitOfWork
{
    IRepository<Actor> Actors { get; }
    IRepository<Basket> Baskets { get; }
    IRepository<Category> Categories { get; }
    IRepository<Product> Products { get; }
    IRepository<Sector> Sectors { get; }
    IRepository<Supplier> Suppliers { get; }
    IRepository<SupplierProduct> SupplierProducts { get; }
    ValueTask<bool> SaveAsync();
}
