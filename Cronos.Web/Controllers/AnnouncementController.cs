using Cronos.Application.Dtos;
using Cronos.Application.Entities;
using Cronos.Application.Features.Announcement;
using Cronos.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Cronos.Application.Features.Announcement.GetAnnouncements;
using static Cronos.Application.Features.Announcement.SaveAnnouncement;
using static Cronos.Application.Features.Announcement.UpdateAnnouncement;
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

        
        //create
        [HttpGet]
        [Route("cms/announcement/saveannouncement")]
        public async Task<IActionResult> SaveAnnouncement()
        {

            return View();
        }

        [HttpPost]
        [Route("cms/announcement/saveannouncement")]
        public async Task<IActionResult> SaveAnnouncement([FromForm] CreateAnnouncementDto obj)
        {
            var result = await _mediator.Send(new SaveAnnouncementCommand(obj));
            return Redirect("announcementlist");
        }

        //edit
        [HttpGet]
        [Route("cms/announcement/updateannouncement")]
        public async Task<IActionResult> UpdateAnnouncement(int id)
        {
            AnnouncementEntity entity = await _mediator.Send(new GetAnnouncementByIdQuery(id));
            return View(entity);
        }

        [HttpPost]
        [Route("cms/announcement/updateannouncement")]
        public async Task<IActionResult> UpdateAnnouncement([FromForm] AnnouncementEntity obj)
        {
            await _mediator.Send(new UpdateAnnouncementCommand(obj));
            return Redirect("announcementlist");
        }
        //delete
        [Route("cms/announcement/deleteannouncement")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            await _mediator.Send(new DeleteAnnouncementCommand(id));
            return Redirect("announcementlist");
        }
    }
}
