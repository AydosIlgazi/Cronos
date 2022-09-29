using Cronos.Application.Data;
using Cronos.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Banner
{
    //22.09.2022 Irem Kesemen
    public class GetBannerById : IRequest<BannerUpdateViewModel>
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public string RedirectUrl { get; set; }
        public class GetBannerByIdHandler : IRequestHandler<GetBannerById, BannerUpdateViewModel>
        {
            private readonly ApplicationContext _context;

            private readonly IMapper _mapper;
            public GetBannerByIdHandler(ApplicationContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }
            public async Task<BannerUpdateViewModel> Handle(GetBannerById query, CancellationToken cancellationToken)
            {
                var banner = _context.Banners.Where(a => a.Id == query.Id).AsNoTracking().FirstOrDefault();
              
                if (banner == null) return null;


                //1.yol
                //BannerUpdateViewModel bannerUpdate = new BannerUpdateViewModel()
                //{
                       //hepsini teker teker girmek gerekir ama map bunu bizim yerimize yapıyor.
                //    AltText = banner.AltText,

                //};

                //2.yol general profileda mapledikten sonra buradan maplemek
                BannerUpdateViewModel bannerUpdate = _mapper.Map<BannerUpdateViewModel>(banner);
               
                return bannerUpdate;
            }
        }
    }

}
