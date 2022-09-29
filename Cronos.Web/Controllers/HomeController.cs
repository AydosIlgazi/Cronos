using Cronos.Application.ViewModels;
using Cronos.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Cronos.Application.Features.Activity.GetActivities;
using static Cronos.Application.Features.Banner.GetBanners;
using static Cronos.Application.Features.Announcement.GetAnnouncements;

namespace Cronos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Route("cms/home/index")]
        public async Task<IActionResult> Index()
        
        {
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.BannerViewModel = await _mediator.Send(new GetBannersQuery());
            viewModel.AnnouncementCardViewModel = await _mediator.Send(new GetAnnouncementCardQuery());
            viewModel.ActivityViewModel = await _mediator.Send(new GetActivitiesQuery());
            System.Diagnostics.Debug.WriteLine(viewModel);
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}