namespace Footwear.Addons
{
    using AutoMapper;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;

    public class AutoMapperProfile : Profile
    {
        public void RegisterUserProfile()
        {
            CreateMap<RegisterViewModel, User>();
        }
    }
}
