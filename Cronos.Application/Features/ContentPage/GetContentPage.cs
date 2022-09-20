
namespace Cronos.Application.Features.ContentPage
{
    public class GetContentPage
    {
        public class GetContentPageQuery: IRequest<ContentPageViewModel>
        {
            public string Url { get; set; }
        }
        public class GetContentPageHandler : IRequestHandler<GetContentPageQuery, ContentPageViewModel>
        {
            private readonly ApplicationContext _context;
            public GetContentPageHandler(ApplicationContext context)
            {
                _context = context;
            }

            public async Task<ContentPageViewModel> Handle(GetContentPageQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => new ContentPageViewModel());
                //return Task.FromResult<ContentPageViewModel>( new ContentPageViewModel());
                //return new ContentPageViewModel(); 
            }
        }
        public class ContentPageViewModel
        {
            public string Title { get; set; }
        }
    }
}
