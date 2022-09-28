

using Cronos.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using static Cronos.Application.Features.Banner.GetBanners;

namespace Cronos.Application.ViewModels
{
    public class BannerViewModel
    {
        //23.09.2022 Irem Kesemen
        public int Id { get; set; }

        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public string RedirectUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
        public DateTime ModifiedDate { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        /// 



        public List<BannerDto> Banners { get; set; }

 
    }
}
