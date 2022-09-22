using Cronos.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cronos.Application.Features.Announcement.GetAnnouncements;
using static Cronos.Application.Features.Banner.GetBanners;

namespace Cronos.Web.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IMediator _mediator;
        public AnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("cms/announcement/announcementlist")]
        public async Task<IActionResult> Index()
        {
            AnnouncementViewModel viewModel = new();
            viewModel = await _mediator.Send(new GetAnnouncementQuery());
            return View(viewModel);
        }

        //edit
        //create
        //delete

    }
}
