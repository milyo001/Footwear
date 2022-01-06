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

            //Get user information
            CreateMap<User, UserProfileDataViewModel>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country));

            CreateMap<ProfileUpdateViewModel, User>()
                .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
                .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.City))
                .ForPath(dest => dest.Address.State, opt => opt.MapFrom(src => src.State))
                .ForPath(dest => dest.Address.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForPath(dest => dest.Address.Country, opt => opt.MapFrom(src => src.Country));

            CreateMap<EmailViewModel, User>();
        }       
    }
}
