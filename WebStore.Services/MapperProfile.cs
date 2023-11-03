using AutoMapper;
using WebStore.Model;
using WebStore.ViewModels.VM;
namespace WebStore.Services
{
    public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<Product, ProductVm>();
		CreateMap<AddOrUpdateProductVm, Product>();

		CreateMap<Order, OrderVm>();

		CreateMap<Invoice, InvoiceVm>();

		CreateMap<Address, AddressVm>();
		CreateMap<AddOrUpdateAddressVm, Address>();
	}
}
}