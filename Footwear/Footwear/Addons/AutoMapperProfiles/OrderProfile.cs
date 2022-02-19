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
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UserProfileDataViewModel, BillingInformation>();

            CreateMap<AppData, DeliveryInfoViewModel>();

        }
    }
}
