using AlphaShop.Domain.Entities;
using AlphaShop.Web.ViewModels.Product;
using AutoMapper;

namespace AlphaShop.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductVM>().ReverseMap();
        }
    }
}
