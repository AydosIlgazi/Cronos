using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cronos.Application.Features.ContentPage.GetContentPage;

namespace Cronos.Web.Controllers
{
    
    public class ContentPageController : Controller
    {
        private readonly IMediator _mediator;
        public ContentPageController(IMediator mediator)
        {
            _mediator = mediator;
        }

     
        public async Task<IActionResult> Index(string url)
        {
            GetContentPageQuery query = new GetContentPageQuery()
            {
                Url = url
            };
            var contentViewModel = await _mediator.Send(query);
            contentViewModel.Title = url;
            return View(contentViewModel);
        }
    }
}
