using Bulk.DataAccess.Contexts;
using Bulk.DataAccess.Repositrories;
using Bulk.Domain.Entities;

namespace Bulk.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IRepository<Actor> Actors { get; }
    public IRepository<Basket> Baskets { get; }
    public IRepository<Category> Categories { get; }
    public IRepository<Product> Products { get; }
    public IRepository<Sector> Sectors { get; }
    public IRepository<Supplier> Suppliers { get; }
    public IRepository<SupplierProduct> SupplierProducts { get; }
    public UnitOfWork(AppDbContext context)
    {
        this._context = context;
        Actors = new Repository<Actor>(_context);
        Baskets = new Repository<Basket>(_context);
        Categories = new Repository<Category>(_context);
        Products = new Repository<Product>(_context);
        Sectors = new Repository<Sector>(_context);
        Suppliers = new Repository<Supplier>(_context);
        SupplierProducts = new Repository<SupplierProduct>(_context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
