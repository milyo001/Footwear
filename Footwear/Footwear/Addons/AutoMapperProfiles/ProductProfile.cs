namespace Footwear.Addons.AutoMapperProfiles
{
    using AutoMapper;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<RegisterViewModel, User>()
        }
    }
}
