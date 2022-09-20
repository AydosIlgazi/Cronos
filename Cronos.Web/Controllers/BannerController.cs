using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cronos.Application.Features.Banner.GetBanners;

namespace Cronos.Web.Controllers
{
    public class BannerController : Controller
    {
        private readonly IMediator _mediator;
        public BannerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //edit
        //create
        //delete

    }
}
