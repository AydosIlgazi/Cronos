
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

        }
    }
}
