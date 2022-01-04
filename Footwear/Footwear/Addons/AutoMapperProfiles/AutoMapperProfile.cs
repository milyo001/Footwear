namespace Footwear.Addons
{
    using AutoMapper;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Create/register User
            CreateMap<RegisterViewModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Address, opt =>
                opt.MapFrom(src => new Address { City = "", Street = "", Country = "", State = "", ZipCode = "" }))
                .ForMember(dest => dest.Cart, opt => opt.MapFrom(src => new Cart { }));

            //Modfiy user information
            CreateMap<UserProfileDataViewModel, User>()
                .ForMember(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.Address.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Address.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Address.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.Address.Country, opt => opt.MapFrom(src => src.Country));
                

        }       
    }
}
