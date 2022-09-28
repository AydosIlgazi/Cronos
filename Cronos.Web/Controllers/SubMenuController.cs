using Cronos.Application.ViewModels;
using Cronos.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cronos.Application.Features.Menu.GetSubMenus;

namespace Cronos.Web.Controllers
{
    public class SubMenuController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public SubMenuController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Route("cms/submenu/list")]
        public async Task<IActionResult> List()
        {
            SubMenuViewModel viewModel = new SubMenuViewModel();

            viewModel = await _mediator.Send(new GetSubMenusQuery());


            return View(viewModel);
        }


       

        [Route("cms/submenu/edit")]
        public async Task<IActionResult> Edit()
        {
            SubMenuViewModel viewModel = new SubMenuViewModel();

            viewModel = await _mediator.Send(new GetSubMenusQuery());


            return View(viewModel);
        }

       
        [Route("cms/submenu/edit")]
        [HttpPost]
        public IActionResult Edit(IFormCollection collection)
        {
            System.Diagnostics.Debug.WriteLine("test");



            return RedirectToAction("Index", "Home");
        }

    }
}
