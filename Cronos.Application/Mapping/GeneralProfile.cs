
namespace Cronos.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<BannerEntity, BannerDto>();
            CreateMap<AnnouncementEntity, AnnouncementDto>();
            CreateMap<AnnouncementEntity, AnnouncementCardDto>();
            CreateMap<AnnouncementEntity, CreateAnnouncementDto>().ReverseMap();
            CreateMap<UpdateAnnouncementDto, AnnouncementEntity>().ReverseMap();

            //28.09.2022 Irem Kesemen
            CreateMap<BannerEntity, BannerUpdateViewModel>().ReverseMap();
        }
    }
}
