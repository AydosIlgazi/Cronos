
namespace Cronos.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<BannerEntity, BannerDto>();

            //28.09.2022 Irem Kesemen
            CreateMap<BannerEntity, BannerUpdateViewModel>().ReverseMap();
        }
    }
}
