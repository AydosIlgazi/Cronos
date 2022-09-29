using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Banner
{
    public class GetBannersCMS
    {
        //27.09.2022 Irem Kesemen

        public class GetBannersCMSQuery : IRequest<BannerViewModel>
        {

        }
        public class GetBannersCMSQueryHandler : IRequestHandler<GetBannersCMSQuery, BannerViewModel>
        {

            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public GetBannersCMSQueryHandler(ApplicationContext context, IMapper mapper)
            {     
                _context = context;
                _mapper = mapper;     
            }
            public async Task<BannerViewModel> Handle(GetBannersCMSQuery request, CancellationToken cancellationToken)
            {
                var banners = await _context.Banners.DisplayedEntitiesCms().ToListAsync(cancellationToken);

                BannerViewModel bannerViewModel = new BannerViewModel()
                {
                    Banners = _mapper.Map<List<BannerDto>>(banners)
                };

                return bannerViewModel;
            }
        }
    }
}
