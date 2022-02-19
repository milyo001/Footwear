namespace Footwear.Addons.AutoMapperProfiles
{
    using AutoMapper;
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Footwear.ViewModels;
    using System;

    public class CartProfile : Profile
    {
        public CartProfile()
        {

            CreateMap<CartProduct, CartProductViewModel>()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Value))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.ToString()));

            CreateMap<CartProductViewModel, CartProduct>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (Gender)Enum.Parse(typeof(Gender), src.Gender)))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src =>
                (ProductType)Enum.Parse(typeof(ProductType), src.ProductType)));
        }
    }
}
