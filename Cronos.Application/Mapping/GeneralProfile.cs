
using Cronos.Application.Entities.Menu;

namespace Cronos.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<BannerEntity, BannerDto>();
            CreateMap<Menu, MenusDto>();
            CreateMap<SubMenu, SubMenusDto>();
            CreateMap<SubMenu2, SubMenus2Dto>();
            CreateMap<MenusDto, Menu>();
        }
    }
}
