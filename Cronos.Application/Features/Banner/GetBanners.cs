namespace Cronos.Application.Features.Banner
{
    public class GetBanners
    {
        public class GetBannersQuery : IRequest<BannerViewModel>
        {
            
        }
        public class GetBannersHandler : IRequestHandler<GetBannersQuery, BannerViewModel>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetBannersHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BannerViewModel> Handle(GetBannersQuery request, CancellationToken cancellationToken)
            {

                var banners = await _context.Banners.DisplayedEntities().ToListAsync(cancellationToken);

                BannerViewModel bannerViewModel = new BannerViewModel()
                {
                    Banners = _mapper.Map<List<BannerDto>>(banners)
                };

                return bannerViewModel;
            }
            
        }


    }
}
