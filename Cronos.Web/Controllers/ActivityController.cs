using Cronos.Application.Data;
using Cronos.Application.Features.Activity;
using Cronos.Application.Migrations;
using Cronos.Application.Validations;
using Cronos.Application.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cronos.Application.Features.Activity.GetActivities;

namespace Cronos.Web.Controllers
{

    
    public class ActivityController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ApplicationContext _context;
        public ActivityController(IMediator mediator, ApplicationContext context)
        {
            _mediator = mediator;   
            _context = context;
        }
        [Route("cms/activity/activityList")]

        public async Task<IActionResult> Index()
        {
           ActivityViewModel activityViewModel = new ActivityViewModel();
           activityViewModel = await _mediator.Send(new GetAllActivityQuery());
           return View(activityViewModel);

        }
        [Route("cms/activity/addActivity")]
        [HttpGet]
        public async Task<IActionResult> AddActivity()
        {
            return View();
        }
        [HttpPost]
        [Route("cms/activity/addActivity")]
        public async Task<IActionResult> AddActivity(AddActivityCommand command)
        {
            CreateActivityValidator validator = new CreateActivityValidator();
            ValidationResult result = validator.Validate(command);
            if (result.IsValid)
            {
            var activityViewModel = await _mediator.Send(command);
            TempData["success"] = "İşlem başarılı";
              
            }
            
            else
            {
                foreach (var state in ModelState.Values)
                {
                    if (state.Errors.Count > 0)
                        state.Errors.Clear();
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [Route("cms/activity/updateActivity")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var activity = await _mediator.Send(new GetActivityByIdQuery { Id = id });
           
            return View(activity);
        }

        [Route("cms/activity/updateActivity")]
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateActivityCommand command)
        {
            UpdateActivityValidator validator = new UpdateActivityValidator();
            ValidationResult result = validator.Validate(command);
            command.Id = id;

            if (result.IsValid)
            {
            var activity = await _mediator.Send(command);
            TempData["success"] = "İşlem başarılı";

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id, DeleteActivityCommand command)
        {
            command.Id = id;
            var activity = await _mediator.Send(command);
            TempData["success"] = "İşlem başarılı";
            return RedirectToAction("Index", "Activity");
        }

   
    }
}
