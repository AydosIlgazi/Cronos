
using Cronos.Application.Dtos;
using static Cronos.Application.Features.Banner.GetBanners;

namespace Cronos.Application.ViewModels
{
    public class BannerViewModel
    {
        public List<BannerDto> Banners { get; set; }
    }
}
