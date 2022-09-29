
using Cronos.Application.Dtos;
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
            CreateMap<SubMenusDto, SubMenu>();
            CreateMap<Menu, SubMenusDto>();
            CreateMap<SubMenus2Dto, SubMenu2>();
            CreateMap<SubMenu2, SubMenus2Dto>();
        }
    }
}
