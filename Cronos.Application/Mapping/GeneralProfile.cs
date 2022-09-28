
namespace Cronos.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<BannerEntity, BannerDto>();
            CreateMap<AnnouncementEntity, AnnouncementDto>();
            CreateMap<AnnouncementEntity, AnnouncementCardDto>();
            CreateMap<AnnouncementEntity, CreateAnnouncementDto>();
            CreateMap<CreateAnnouncementDto, AnnouncementEntity>();
            CreateMap<UpdateAnnouncementDto, AnnouncementEntity>();
            CreateMap<AnnouncementEntity, UpdateAnnouncementDto> ();
        }
    }
}
