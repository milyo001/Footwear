namespace Footwear.Addons
{
    using AutoMapper;
    using Footwear.ViewModels;
    using Footwear.Data.Models;
    using System;
    using Footwear.Data.Models.Enums;

    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderViewModel, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (Status)Enum.Parse(typeof(Status), src.Status)))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UserData, opt =>
                opt.MapFrom(src => new BillingInformation()));

            CreateMap<UserProfileDataViewModel, BillingInformation>();

        }
    }
}
