namespace Footwear.Addons
{
    using AutoMapper;
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Footwear.ViewModels;
    using System;

    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderViewModel, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (Status)Enum.Parse(typeof(Status), src.Status)))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow.Date));

            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.CartProducts, opt => opt.MapFrom(src => src.Products));

            CreateMap<UserProfileDataViewModel, BillingInformation>();
            CreateMap<BillingInformation, UserProfileDataViewModel>();

            CreateMap<AppData, DeliveryInfoViewModel>();

        }
    }
}
