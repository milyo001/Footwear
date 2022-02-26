namespace Footwear.Addons.AutoMapperProfiles
{
    using AutoMapper;
    using Footwear.Data.Models;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // The default cartProduct quantity when added to cart
             const int defaultItemQuantity = 1;

            CreateMap<Product, CartProduct>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(p => p.ProductImage.ImageUrl))
                .ForMember(x => x.ProductId, opt => opt.MapFrom(p => p.Id))
                .ForMember(x => x.Quantity, opt => opt.MapFrom(src => defaultItemQuantity));
            
        }
    }
}

