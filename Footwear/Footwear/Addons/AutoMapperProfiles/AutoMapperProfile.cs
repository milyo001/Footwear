namespace Footwear.Addons
{
    using AutoMapper;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Create/register User
            CreateMap<RegisterViewModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Address, opt =>
                opt.MapFrom(src => new Address { City = "", Street = "", Country = "", State = "", ZipCode = "" }))
                .ForMember(dest => dest.Cart, opt => opt.MapFrom(src => new Cart { }));
        }
    }
}
