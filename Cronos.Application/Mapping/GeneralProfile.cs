
using Cronos.Application.Dtos;
using Cronos.Application.Entities.Menu;

using Cronos.Application.Dtos.Activity;

namespace Cronos.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<BannerEntity, BannerDto>();
            CreateMap<ActivityEntity, ActivityDto>();
            CreateMap<ActivityEntity, UpdateActivityViewModel>().ReverseMap();
            CreateMap<AnnouncementEntity, AnnouncementDto>();
            CreateMap<AnnouncementEntity, AnnouncementCardDto>();
            CreateMap<AnnouncementEntity, CreateAnnouncementDto>().ReverseMap();
            CreateMap<UpdateAnnouncementDto, AnnouncementEntity>().ReverseMap();

            //28.09.2022 Irem Kesemen
            CreateMap<BannerEntity, BannerUpdateViewModel>().ReverseMap();
            CreateMap<Menu, MenusDto>();
            CreateMap<SubMenu, SubMenusDto>();
            CreateMap<SubMenu2, SubMenus2Dto>();
            CreateMap<MenusDto, Menu>();
            CreateMap<SubMenusDto, SubMenu>();
            CreateMap<Menu, SubMenusDto>();
            CreateMap<SubMenus2Dto, SubMenu2>();
            CreateMap<SubMenu2, SubMenus2Dto>();
        }
    }
}
