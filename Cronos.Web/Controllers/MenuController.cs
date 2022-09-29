using Cronos.Application.Dtos;
using Cronos.Application.Features.Menu;
using Cronos.Application.ViewModels;
using Cronos.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cronos.Application.Features.Menu.GetAllMenus;
using static Cronos.Application.Features.Menu.GetMenus;
using static Cronos.Application.Features.Menu.UpdateMenu;
using static Cronos.Application.Features.Menu.CreateMenu;
using Cronos.Application.Entities.Menu;
using NuGet.Protocol;
using System.Text.Json.Nodes;

namespace Cronos.Web.Controllers
{
    public class MenuController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public MenuController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }




        //localhost/Menu/Edit shows menus in table | Ş.Geyik | 23.09.22

        [Route("cms/menu/edit")]
        public async Task<IActionResult> Edit()
        {
            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());


            return View(viewModel.MenuViewModel);
        }



        [Route("cms/menu/addMenu")]
        [HttpGet]
        public async Task<IActionResult> AddMenu()
        {

            TempData["result"] = "";
            
            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());

            TempData["maxOrder"] = viewModel.MenuViewModel.Menus.OrderByDescending(menu=>menu.Order).First().Order+1;

            return View(viewModel.MenuViewModel);
        }



        [Route("cms/menu/addMenu/")]
        [HttpPost]
        public async Task<IActionResult?> AddMenu(MenusDto menu)
        {

            if (ModelState.IsValid)
            {
                var isSaved = await _mediator.Send(new CreateMenuCommand(menu));

                if (isSaved == true)
                {
                    TempData["result"] = "success";


                    return View(menu);
                }
                else
                {
                    TempData["result"] = "fail";
                    return View(menu);
                }


            }

            //model is not valid show validations error || Ş.Geyik 27.09.22
            else
            {
                TempData["result"] = "fail";
                return View(menu);
            }







        }



        [Route("cms/menu/editMenu/{id:int}")]
        [HttpGet]
        public async Task<IActionResult?> EditMenu(int id)
        {

            MenuViewModel viewModel = new MenuViewModel();

            viewModel = await _mediator.Send(new GetMenusQuery());

            var menu = viewModel.Menus.Find(menu => menu.Id == id);

            if (menu == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Error Not Found 404");
            }

            //islem basarili menu güncellendi  || Ş.Geyik 27.09.22
            else
            {


                return View(menu);

            }


        }




        [Route("cms/menu/editMenu/{id:int}")]
        [HttpPost]
        public async Task<IActionResult?> EditMenu(MenusDto menu)
        {

            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateMenuCommand(menu));

                TempData["result"] = "success";
                return View(menu);
            }


            //model is not valid show validations error || Ş.Geyik 27.09.22
            else
            {
                TempData["result"] = "fail";
                return View(menu);
            }







        }





        [Route("cms/menu/deleteMenu/{id:int}")]
        [HttpGet]
        public async Task<IActionResult?> DeleteMenu(int id)
        {

            MenuViewModel viewModel = new MenuViewModel();

            viewModel = await _mediator.Send(new GetMenusQuery());

            var menu = viewModel.Menus.Find(menu => menu.Id == id);

            if (menu == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Error Not Found 404");
            }

            //islem basarili menu silindi
            else
            {


                return View(menu);

            }


        }



        [Route("cms/menu/deleteMenu/{id:int}")]
        [HttpPost]
        public async Task<IActionResult?> DeleteMenu(MenusDto menu)
        {


            //set order to max order deleted menu before deleting process Ş.geyik || 28.09.22
            //MenuViewModel viewModel = new MenuViewModel();

            //viewModel = await _mediator.Send(new GetMenusQuery());

 
        
            //    menu.Order = viewModel.Menus..OrderByDescending(menu => menu.Order).First().Order + 1;
            //    await _mediator.Send(new UpdateMenuCommand(menu));
            
          

          

            if (menu != null)
            {
                await _mediator.Send(new DeleteMenuCommand(menu.Id));

                TempData["result"] = "success";
                return View(menu);
            }

            //model is not valid show validations error
            else
            {
                TempData["result"] = "fail";
                return View(menu);
            }







        }









        [Route("cms/menu/list")]
        public async Task<IActionResult> List()
        {
            HomeViewModel viewModel = new HomeViewModel();

            viewModel.MenuViewModel = await _mediator.Send(new GetAllMenusQuery());


            return View(viewModel.MenuViewModel);
        }



        [Route("cms/menu/edit")]
        [HttpPost]
        public IActionResult Edit(IFormCollection collection)
        {


            System.Diagnostics.Debug.WriteLine("test edit cms/menu/edit");



            return RedirectToAction("Index", "Home");
        }



        /*---APIS START HERE Ş.Geyik 28.09.22------*/






        //add api folder if its neccessary
        [Route("api/menu/getAllMenusByOrder")]
        [HttpGet]
        public async Task<string> GetAllMenusByOrder()
        {



            MenuViewModel viewModel = new MenuViewModel();

            viewModel = await _mediator.Send(new GetMenusQuery());

            return viewModel.Menus.ToJson();

        }

        [Route("api/menu/saveMenuOrders")]
        [HttpPost]
        public async Task SaveMenuOrders([FromBody] MenusDto[] menus)
        {
            MenuViewModel viewModel = new MenuViewModel();

            viewModel = await _mediator.Send(new GetMenusQuery());

            foreach (var menuOrdered in menus)
            {

                

                var menu = viewModel.Menus.Find(menu => menu.Id == menuOrdered.Id);

                if (menu != null)
                {
                    menu.Order = menuOrdered.Order;

                   

                    var isSaved = await _mediator.Send(new UpdateMenuCommand(menu));
                  //  System.Diagnostics.Debug.WriteLine("isSaved", isSaved);
                }
       
            }

           
            

            
        }



        /*---APIS END HERE Ş.Geyik 28.09.22------*/





    }
}
