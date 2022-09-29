using Cronos.Application.ViewModels;
using Cronos.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cronos.Application.Features.Menu.GetSubMenus2;

namespace Cronos.Web.Controllers
{
    public class SubMenu2Controller : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public SubMenu2Controller(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Route("cms/submenu2/list")]
        public async Task<IActionResult> List()
        {
            SubMenu2ViewModel viewModel = new SubMenu2ViewModel();

            viewModel = await _mediator.Send(new GetSubMenus2Query());


            return View(viewModel);
        }



        [Route("cms/submenu2/edit")]
        public async Task<IActionResult> Edit()
        {
            SubMenu2ViewModel viewModel = new SubMenu2ViewModel();

            viewModel = await _mediator.Send(new GetSubMenus2Query());


            return View(viewModel);
        }

        [Route("cms/submenu2/edit")]
        [HttpPost]
        public IActionResult Edit(IFormCollection collection)
        {
            System.Diagnostics.Debug.WriteLine("test");



            return RedirectToAction("Index", "Home");
        }
    }
}
