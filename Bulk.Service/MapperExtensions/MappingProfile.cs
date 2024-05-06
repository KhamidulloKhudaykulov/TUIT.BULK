using AutoMapper;
using Bulk.Domain.Entities;
using Bulk.Model.Actors;
using Bulk.Model.Baskets;
using Bulk.Model.Categories;
using Bulk.Model.Products;
using Bulk.Model.Sectors;
using Bulk.Model.SupplierProducts;
using Bulk.Model.Suppliers;

namespace Bulk.Service.MapperExtensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Actor, ActorCreateModel>().ReverseMap();
        CreateMap<Actor, ActorUpdateModel>().ReverseMap();
        CreateMap<Actor, ActorViewModel>().ReverseMap();

        CreateMap<Basket, BasketCreateModel>().ReverseMap();
        CreateMap<Basket, BasketUpdateModel>().ReverseMap();
        CreateMap<Basket, BasketViewModel>().ReverseMap();

        CreateMap<Category, CategoryCreateModel>().ReverseMap();
        CreateMap<Category, CategoryUpdateModel>().ReverseMap();
        CreateMap<Category, CategoryViewModel>().ReverseMap();

        CreateMap<Product, ProductCreateModel>().ReverseMap();
        CreateMap<Product, ProductUpdateModel>().ReverseMap();
        CreateMap<Product, ProductViewModel>().ReverseMap();

        CreateMap<Sector, SectorCreateModel>().ReverseMap();
        CreateMap<Sector, SectorUpdateModel>().ReverseMap();
        CreateMap<Sector, SectorViewModel>().ReverseMap();

        CreateMap<SupplierProduct, SupplierProductCreateModel>().ReverseMap();
        CreateMap<SupplierProduct, SupplierProductUpdateModel>().ReverseMap();
        CreateMap<SupplierProduct, SupplierProductViewModel>().ReverseMap();

        CreateMap<Supplier, SupplierCreateModel>().ReverseMap();
        CreateMap<Supplier, SupplierUpdateModel>().ReverseMap();
        CreateMap<Supplier, SupplierViewModel>().ReverseMap();
    }
}
