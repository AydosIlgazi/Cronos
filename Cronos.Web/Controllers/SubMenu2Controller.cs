using Cronos.Application.Dtos;
using Cronos.Application.ViewModels;
using Cronos.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using static Cronos.Application.Features.Menu.GetAllMenus;
using static Cronos.Application.Features.Menu.GetSubMenus;

using static Cronos.Application.Features.SubMenu2.CreateSubMenu2;
using static Cronos.Application.Features.SubMenu2.UpdateSubMenu2;

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
            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());

            return View(viewModel);
        }




        [Route("cms/submenu2/edit")]
        public async Task<IActionResult> Edit()
        {
            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());


            return View(viewModel);
        }





        [Route("cms/submenu2/addSubMenu2")]
        [HttpGet]
        public async Task<IActionResult> AddSubMenu2()
        {

            TempData["result"] = "";

            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());

            TempData["maxOrder"] = viewModel.MenuViewModel.SubMenus2.OrderByDescending(menu => menu.Order).First().Order + 1;

            return View(viewModel);
        }



        [Route("cms/submenu2/addSubMenu2")]
        [HttpPost]
        public async Task<IActionResult?> AddSubMenu2(SubMenus2Dto submenu)
        {

            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());


            if (ModelState.IsValid)
            {
                var isSaved = await _mediator.Send(new CreateSubMenu2Command(submenu));

                if (isSaved == true)
                {
                    TempData["result"] = "success";


                    return View(viewModel);
                }
                else
                {
                    TempData["result"] = "fail";
                    return View(viewModel);
                }


            }

            //model is not valid show validations error || Ş.Geyik 27.09.22
            else
            {
                TempData["result"] = "fail";
                return View(viewModel);
            }



        }




        [Route("cms/submenu2/editSubMenu2/{id:int}")]
        [HttpGet]
        public async Task<IActionResult?> EditSubMenu2(int id)
        {

            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());

            var submenu2 = viewModel.MenuViewModel.SubMenus2.Find(menu => menu.Id == id);

            if (submenu2 == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Error Not Found 404");
            }

            //islem basarili menu güncellendi  || Ş.Geyik 27.09.22
            else
            {


                return View(submenu2);

            }


        }




        [Route("cms/submenu2/editSubMenu2/{id:int}")]
        [HttpPost]
        public async Task<IActionResult?> EditSubMenu2(SubMenus2Dto submenu)
        {

            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateSubMenu2Command(submenu));

                TempData["result"] = "success";
                return View(submenu);
            }


            //model is not valid show validations error || Ş.Geyik 27.09.22
            else
            {
                TempData["result"] = "fail";
                return View(submenu);
            }



        }


        [Route("cms/submenu2/deleteSubMenu2/{id:int}")]
        [HttpGet]
        public async Task<IActionResult?> DeleteSubMenu2(int id)
        {


            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());

            var submenu2 = viewModel.MenuViewModel.SubMenus2.Find(menu => menu.Id == id);



            if (submenu2 == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Error Not Found 404");
            }

            //islem basarili menu silindi Ş.geyik ||29.09.22
            else
            {


                return View(submenu2);

            }


        }



        [Route("cms/submenu2/deleteSubMenu2/{id:int}")]
        [HttpPost]
        public async Task<IActionResult?> DeleteSubMenu2(SubMenus2Dto submenu)
        {


            if (submenu != null)
            {
                await _mediator.Send(new DeleteSubMenu2Command(submenu.Id));

                TempData["result"] = "success";
                return View(submenu);
            }

            //model is not valid show validations error
            else
            {
                TempData["result"] = "fail";
                return View(submenu);
            }


        }



        /*---APIS START HERE Ş.Geyik 29.09.22------*/



        //add api folder if its neccessary
        [Route("api/submenu2/getAllSubMenus2ByOrder/{ParentId:int}")]
        [HttpGet]
        public async Task<string> GetAllSubMenusByOrder(int ParentId)
        {
            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());



            return viewModel.MenuViewModel.SubMenus2.Where(submenu => submenu.ParentId == ParentId).ToJson();

        }

        [Route("api/submenu2/saveSubMenu2Orders")]
        [HttpPost]
        public async Task SaveSubMenuOrders([FromBody] SubMenus2Dto[] submenus)
        {
            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());

            foreach (var subMenuOrdered in submenus)
            {

                var submenu = viewModel.MenuViewModel.SubMenus2.Find(x => x.Id == subMenuOrdered.Id);

                if (submenu != null)
                {
                    submenu.Order = subMenuOrdered.Order;



                    var isSaved = await _mediator.Send(new UpdateSubMenu2Command(submenu));
                   // System.Diagnostics.Debug.WriteLine("isSaved Sub Menu:", isSaved);
                }

            }

        }



        /*---APIS END HERE Ş.Geyik 29.09.22------*/
    }
}
