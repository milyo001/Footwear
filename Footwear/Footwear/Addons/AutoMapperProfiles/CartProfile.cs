namespace Footwear.Addons.AutoMapperProfiles
{
    using AutoMapper;
    using Footwear.Data.Models;
    using Footwear.ViewModels;

    public class CartProfile : Profile
    {
        //.Select(cp => new CartProductViewModel
        //{
        //    Id = cp.Id,
        //    ProductId = cp.ProductId,
        //    Name = cp.Name,
        //    Size = cp.Size.Value,
        //    Gender = cp.Gender.ToString(),
        //    Details = cp.Details,
        //    ImageUrl = cp.ImageUrl,
        //    Price = cp.Price,
        //    Quantity = cp.Quantity,
        //    ProductType = cp.ProductType.ToString()
        //})
        public CartProfile()
        {
            CreateMap<CartProduct, CartProductViewModel>()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Value))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.ToString()));

        }
    }
}
