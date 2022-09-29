using Cronos.Application.Data.Configurations;
using Cronos.Application.Entities;
using Cronos.Application.Features.Banner;
using Cronos.Application.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net;
using System.Xml.Linq;
using static Cronos.Application.Features.Banner.GetBanners;
using static Cronos.Application.Features.Banner.GetBannersCMS;
using static Cronos.Application.Features.Banner.SaveBanners;
using static Cronos.Application.Features.Banner.UpdateBanners;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cronos.Web.Controllers
{
    
    public class BannerController : Controller
    {
        private IValidator<BannerValidator> _validator;
        private readonly IMediator _mediator;
        public BannerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //edit
        //create
        //delete

        //22.09.2022 Irem Kesemen
        //[Route("cms/banner/list")]

        [Route("cms/Banner/Index")]
        public async Task<IActionResult> Index()
        {
            //Tüm bannerlerin gösterildiği liste. İrem Kesemen 22/09/2022
            BannerViewModel viewModel = new BannerViewModel();
           
             viewModel = await _mediator.Send(new GetBannersCMSQuery());

            return View(viewModel);
        }

        //Bannerlerin oluşturulması İrem Kesemen 22/09/20222

        [Route("cms/banner/create")]
        public async Task<IActionResult> Create(SaveBannersCommand command, BannerEntity entity,int id)
        {
            BannerValidator validations = new BannerValidator();
            ValidationResult results = validations.Validate(entity);

            ModelState.Clear();
            return View();
        }
        [Route("cms/banner/create")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveBannersCommand command, BannerEntity entity)
        {

            BannerValidator validations = new BannerValidator();
            ValidationResult result = validations.Validate(entity);
            if (ModelState.IsValid)
            {

                TempData["success"] = "İşlem Başarılı";
                var viewModel = await _mediator.Send(command);

                //return View();
            }
            else
            {
                foreach (ValidationFailure item in result.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    
                }

                //foreach (var state in ModelState.Values)
                //{
                //    if (state.Errors.Count > 0)
                //        state.Errors.Clear();
                //}

            }
            return View() ;
           
        }


        [HttpGet]
        [Route("cms/banner/update/{id:int}")]
        public async Task<IActionResult> Update(int id,BannerUpdateViewModel entity)
        {
            //TempData["success"] = "İşlem Başarılı!";
            ModelState.Clear();
            UpdateBannerValidator validations = new UpdateBannerValidator();
            ValidationResult results = validations.Validate(entity);

            var viewModel = await _mediator.Send(new GetBannerById { Id = id });
            return View(viewModel);
        }
        [HttpPost]
        [Route("cms/banner/update/{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateBannersCommand command, BannerUpdateViewModel entity)
        {
           if (id != command.Id)
            {
                return BadRequest();
            }
            UpdateBannerValidator validations = new UpdateBannerValidator();
            ValidationResult results = validations.Validate(entity);
            if (ModelState.IsValid)
            {

                TempData["success"] = "İşlem Başarılı!";
                var viewModel = await _mediator.Send(command);

               // return RedirectToAction("Index", "Banner", new { @viewModel = viewModel });
            }
            else
            {
                foreach (ValidationFailure items in results.Errors)
                {
                    ModelState.AddModelError(items.PropertyName, items.ErrorMessage);
                
                }
            }
            return View();
          
           
        }

        [Route("cms/banner/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _mediator.Send(new DeleteBannertByIdCommand(id));
            return RedirectToAction("index", "Banner");
           

        }

    }
}
