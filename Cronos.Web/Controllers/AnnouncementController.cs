using Cronos.Application.Dtos;
using Cronos.Application.Entities;
using Cronos.Application.Features.Announcement;
using Cronos.Application.Validations.Announcement;
using Cronos.Application.ViewModels;
using FluentValidation.Results;
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

        //read
        [Route("cms/announcement/announcementlist")]
        public async Task<IActionResult> Index()
        {
            AnnouncementViewModel viewModel = new();
            viewModel = await _mediator.Send(new GetAnnouncementCmsQuery());
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
            AnnouncementCreateValidator validator = new AnnouncementCreateValidator();
            ValidationResult validationResult = validator.Validate(obj);
            if (validationResult.IsValid)
            {
                bool result = await _mediator.Send(new SaveAnnouncementCommand(obj));
                if(result == true)
                {
                    TempData["success"] = "Announcement saved succesfully.";
                    TempData["controller"] = "Announcement";
                }
                else
                {
                    TempData["error"] = "Something went wrong";
                }
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        //edit
        [HttpGet]
        [Route("cms/announcement/updateannouncement")]
        public async Task<IActionResult> UpdateAnnouncement(int id)
        {
            AnnouncementUpdateViewModel entity = await _mediator.Send(new GetAnnouncementByIdQuery(id));
            return View(entity);
        }

        [HttpPost]
        [Route("cms/announcement/updateannouncement")]
        public async Task<IActionResult> UpdateAnnouncement([FromForm] AnnouncementUpdateViewModel obj)
        {
            AnnouncementValidator validationRules = new AnnouncementValidator();
            ValidationResult validationResult = validationRules.Validate(obj.Announcement);
            if (validationResult.IsValid)
            {
                bool result =await _mediator.Send(new UpdateAnnouncementCommand(obj.Announcement));
                if(result == true)
                {
                    TempData["success"] = "Announcement updated succesfully.";
                    TempData["controller"] = "Announcement";
                }
                else
                {
                    TempData["error"] = "something went wrong";
                }
               
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        //delete
        [Route("cms/announcement/deleteannouncement")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            bool result =await _mediator.Send(new DeleteAnnouncementCommand(id));
            if(result == true)
            {
                TempData["success"] = "Announcement updated succesfully.";
                    TempData["controller"] = "Announcement";
            }
            else
            {
                TempData["error"] = "Something went wrong";
            }
            return Redirect("announcementlist");
        }

       

    }
}
