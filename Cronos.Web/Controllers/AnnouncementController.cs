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
            viewModel = await _mediator.Send(new GetAnnouncementAdminQuery());
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
            AnnouncementEntity entity = await _mediator.Send(new GetAnnouncementByIdQuery(id));
            return View(entity);
        }

        [HttpPost]
        [Route("cms/announcement/updateannouncement")]
        public async Task<IActionResult> UpdateAnnouncement([FromForm] AnnouncementEntity obj)
        {
            AnnouncementValidator validationRules = new AnnouncementValidator();
            ValidationResult validationResult = validationRules.Validate(obj);
            if (validationResult.IsValid)
            {
                await _mediator.Send(new UpdateAnnouncementCommand(obj));
                TempData["success"] = "Announcement updated succesfully.";
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
                TempData["success"] = "Announcement deleted succesfully.";
            }
            else
            {
                TempData["error"] = "Something went wrong";
            }
            return Redirect("announcementlist");
        }

       

    }
}
