using Cronos.Application.Dtos;
using Cronos.Application.Entities.Menu;
using Cronos.Application.ViewModels;
using Cronos.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cronos.Application.Features.SubMenu.CreateSubMenu;
using static Cronos.Application.Features.Menu.GetAllMenus;
using static Cronos.Application.Features.Menu.GetSubMenus;
using static Cronos.Application.Features.Menu.GetMenus;
using static Cronos.Application.Features.Menu.UpdateMenu;
using static Cronos.Application.Features.SubMenu.UpdateSubMenu;
using NuGet.Protocol;

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

       
     


        [Route("cms/submenu/addSubMenu")]
        [HttpGet]
        public async Task<IActionResult> AddSubMenu()
        {

            TempData["result"] = "";

            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());

            TempData["maxOrder"] = viewModel.MenuViewModel.SubMenus.OrderByDescending(menu => menu.Order).First().Order + 1;

            return View(viewModel);
        }



        [Route("cms/submenu/addSubMenu/")]
        [HttpPost]
        public async Task<IActionResult?> AddSubMenu(SubMenusDto submenu)
        {

            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());
           

            if (ModelState.IsValid)
            {
                var isSaved = await _mediator.Send(new CreateSubMenuCommand(submenu));

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




        [Route("cms/submenu/editSubMenu/{id:int}")]
        [HttpGet]
        public async Task<IActionResult?> EditSubMenu(int id)
        {

            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());

            var submenu = viewModel.MenuViewModel.SubMenus.Find(menu => menu.Id == id);

            if (submenu == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Error Not Found 404");
            }

            //islem basarili menu güncellendi  || Ş.Geyik 27.09.22
            else
            {


                return View(submenu);

            }


        }




        [Route("cms/submenu/editSubMenu/{id:int}")]
        [HttpPost]
        public async Task<IActionResult?> EditSubMenu(SubMenusDto submenu)
        {

            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateSubMenuCommand(submenu));

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


        [Route("cms/menu/deleteSubMenu/{id:int}")]
        [HttpGet]
        public async Task<IActionResult?> DeleteSubMenu(int id)
        {


            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());

            var submenu = viewModel.MenuViewModel.SubMenus.Find(menu => menu.Id == id);

           

            if (submenu == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Error Not Found 404");
            }

            //islem basarili menu silindi
            else
            {


                return View(submenu);

            }


        }



        [Route("cms/menu/deleteSubMenu/{id:int}")]
        [HttpPost]
        public async Task<IActionResult?> DeleteSubMenu(SubMenusDto submenu)
        {


            if (submenu != null)
            {
                await _mediator.Send(new DeleteSubMenuCommand(submenu.Id));

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
        [Route("api/submenu/getAllSubMenusByOrder/{ParentId:int}")]
        [HttpGet]
        public async Task<string> GetAllSubMenusByOrder(int ParentId)
        {
            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());



            return viewModel.MenuViewModel.SubMenus.Where(submenu =>submenu.ParentId==ParentId).ToJson();

        }

        [Route("api/submenu/saveSubMenuOrders")]
        [HttpPost]
        public async Task SaveSubMenuOrders([FromBody] SubMenusDto[] submenus)
        {
            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());

            foreach (var subMenuOrdered in submenus)
            {

                var submenu = viewModel.MenuViewModel.SubMenus.Find(x => x.Id == subMenuOrdered.Id);

                if (submenu != null)
                {
                    submenu.Order = subMenuOrdered.Order;



                    var isSaved = await _mediator.Send(new UpdateSubMenuCommand(submenu));
                     System.Diagnostics.Debug.WriteLine("isSaved Sub Menu:", isSaved);
                }

            }

        }



        /*---APIS END HERE Ş.Geyik 29.09.22------*/




    }
}
